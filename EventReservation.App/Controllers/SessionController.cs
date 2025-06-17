using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Page;
using EventReservation.Models.DTO.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace EventReservation.App.Controllers;

[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class SessionController(AppDbContext dbContext, IOverlappingService overlappingService) : ControllerBase
{
    [HttpGet("api/event/{eventId:int}/sessions")]
    public async Task<IActionResult> GetSessions(int eventId, int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalCount = await dbContext.Session.CountAsync(s => s.EventId == eventId);

        //TODO: dodać index na StartTime, w celu przyspieszenia OrderBy

        var sessions = await dbContext.Session
            .Where(s => s.EventId == eventId)
            .Include(s => s.SessionLimit)
            .OrderBy(s => s.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var sessionDto = sessions.Select(x => new SessionWithLimitDto
        {
            Id = x.Id,
            Name = x.Name,
            StartTime = x.StartTime,
            Duration = x.Duration,
            MaxParticipants = x.SessionLimit?.MaxParticipants ?? 0,
            CurrentReserved = x.SessionLimit?.CurrentReserved ?? 0,
        }).ToList();

        var result = new PagedResult<SessionWithLimitDto>
        {
            Items = sessionDto,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return Ok(result);
    }

    [HttpGet("api/sessions/{sessionId:int}")]
    public async Task<IActionResult> GetSingleSession(int sessionId)
    {

        var session = await dbContext.Session
                                    .Include(s => s.SessionLimit)
                                    .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null || session.SessionLimit == null)
        {
            return NotFound();
        }

        var sessionDto = new SessionDto
        {
            Id = session.Id,
            Name = session.Name,
            StartTime = session.StartTime,
            Duration = session.Duration,
            MaxParticipants = session.SessionLimit!.MaxParticipants,
        };
        return Ok(sessionDto);
    }

    [HttpPost("api/event/{eventId:int}/sessions")]
    public async Task<IActionResult> CreateSession(int eventId, [FromBody] SessionDto newSession) //PLSQL
    {
        var eventIdParam = new OracleParameter("p_event_id", eventId);
        var nameParam = new OracleParameter("p_name", newSession.Name);
        var descriptionParam = new OracleParameter("p_description", newSession.Description);
        var startTimeParam = new OracleParameter("p_start_time", newSession.StartTime);
        var durationParam = new OracleParameter("p_duration", newSession.Duration);
        var maxParticipantsParam = new OracleParameter("p_max_participants", newSession.MaxParticipants);
        var newSessionIdParam = new OracleParameter("p_new_session_id", OracleDbType.Int32, ParameterDirection.Output);

        try
        {
            await dbContext.Database.ExecuteSqlRawAsync(
                @"BEGIN 
                  SESSION_PKG.CREATE_SESSION(
                      p_event_id         => :p_event_id,
                      p_name             => :p_name,
                      p_description      => :p_description,
                      p_start_time       => :p_start_time,
                      p_duration         => :p_duration,
                      p_max_participants => :p_max_participants,
                      p_new_session_id   => :p_new_session_id
                  );
              END;",
                eventIdParam, nameParam, descriptionParam, startTimeParam, durationParam, maxParticipantsParam, newSessionIdParam);

            var newId = Convert.ToInt32(newSessionIdParam.Value.ToString());

            newSession.Id = newId;

            return Ok(newSession);
        }
        catch (OracleException ex)
        {
            if (ex.Number >= 20000 && ex.Number <= 20999)
            {
                switch (ex.Number)
                {
                    case 20001: 
                        return NotFound(ex.Message);

                    case 20002: 
                        return BadRequest(ex.Message);

                    default:
                        return StatusCode(500);
                }
            }
            return StatusCode(500);
        }
    }

    [HttpPut("api/sessions/{sessionId:int}")]
    public async Task<IActionResult> UpdateSession(int sessionId, [FromBody] UpdateSessionDto updatedSession)
    {
        var sessionFromDb = await dbContext.Session
                                 .Include(s => s.Event)
                                 .ThenInclude(e => e.Sessions)
                                 .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (sessionFromDb == null || sessionFromDb.Event == null)
            return NotFound();

        MapSessionDtoToEntity(updatedSession, sessionFromDb);

        if (!IsSessionTimeWithinEventRange(sessionFromDb.Event, sessionFromDb))
            return BadRequest("Invalid session time");

        var otherSessions = sessionFromDb.Event.Sessions
                                                .Where(s => s.Id != sessionId)
                                                .ToList();

        if (overlappingService.AreSessionsOverlapping(otherSessions, sessionFromDb))
            return BadRequest("Session time overlaps with existing session");

        await dbContext.SaveChangesAsync();
        return Ok(sessionFromDb);
    }

    [HttpDelete("api/sessions/{sessionId:int}")]
    public async Task<IActionResult> DeleteSession(int sessionId)
    {
        var session = await dbContext.Session.FindAsync(sessionId);
        if (session == null)
        {
            return NotFound();
        }

        dbContext.Session.Remove(session);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    private void MapSessionDtoToEntity(UpdateSessionDto sessionDto, Session session)
    {
        session.Name = sessionDto.Name;
        session.Description = sessionDto.Description;
        session.StartTime = sessionDto.StartTime ?? session.StartTime;
        session.Duration = sessionDto.Duration ?? session.Duration;
    }
    private bool IsSessionTimeWithinEventRange(Event eventFromDb, SessionDto newSession)
    {
        var createdSessionStartTime = newSession.StartTime;
        var createdSessionEndTime = createdSessionStartTime.AddMinutes(newSession.Duration);

        return createdSessionStartTime >= eventFromDb.StartTime &&
               createdSessionEndTime <= eventFromDb.EndTime;
    }
    private bool IsSessionTimeWithinEventRange(Event eventFromDb, Session newSession)
    {
        var createdSessionStartTime = newSession.StartTime;
        var createdSessionEndTime = createdSessionStartTime.AddMinutes(newSession.Duration);

        return createdSessionStartTime >= eventFromDb.StartTime &&
               createdSessionEndTime <= eventFromDb.EndTime;
    }

}
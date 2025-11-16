using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Event;
using EventReservation.Models.DTO.Page;
using EventReservation.Models.DTO.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //TODO: Dodać tu sessionLimit, bo nie mam pomysłu jak
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
        var session = await dbContext.Session.FindAsync(sessionId);
        // Wyświetlenie limitu uczestników - działa, ale zastanawiam się czy nie można zrobić jakoś include do session bez drugiego zapytania
        var sessionLimit = await dbContext.SessionLimit.Where(sl => sl.SessionId == sessionId).FirstOrDefaultAsync();

        if (session == null)
        {
            return NotFound();
        }

        var sessionDto = new SessionDto
        {
            Id = session.Id,
            Name = session.Name,
            StartTime = session.StartTime,
            Duration = session.Duration,
            MaxParticipants = sessionLimit.MaxParticipants,
        };
        return Ok(sessionDto);
    }

    [HttpPost("api/event/{eventId:int}/sessions")]
    public async Task<IActionResult> CreateSession(int eventId, [FromBody] SessionDto newSession)
    {
        var eventFromDb = await dbContext.Event.Include(e => e.Sessions).Where(e => e.Id == eventId).FirstOrDefaultAsync();
        if (eventFromDb == null)
        {
            return BadRequest("No event");
        }

        if (!IsSessionTimeWithinEventRange(eventFromDb, newSession))
        {
            return BadRequest("Invalid session time");
        }

        if (!eventFromDb.IsOverLappingAllowed && eventFromDb.Sessions != null)
        {
            var sessions = eventFromDb.Sessions;
            if (overlappingService.AreSessionsOverlapping(sessions, newSession))
            {
                return BadRequest("Session time overlaps with existing session");
            }
        }

        var createdSession = new Session
        {
            EventId = eventId,
            Name = newSession.Name,
            Description = newSession.Description,
            StartTime = newSession.StartTime,
            Duration = newSession.Duration,
        };

        await dbContext.Session.AddAsync(createdSession);
        await dbContext.SaveChangesAsync();

        var sessionLimit = new SessionLimit
        {
            SessionId = createdSession.Id,
            MaxParticipants = newSession.MaxParticipants,
            CurrentReserved = 0
        };

        await dbContext.SessionLimit.AddAsync(sessionLimit);
        await dbContext.SaveChangesAsync();

        //TODO: Poprawione Id, bo zwracało z Dto zamiast poprawnego z bazy, nie wiem czy tak czy inaczej to zrobić
        newSession.Id = createdSession.Id;

        //return CreatedAtAction(nameof(GetSingleSession), new { sessionId = createdSession.Id }, createdSession);
        return Ok(newSession);
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
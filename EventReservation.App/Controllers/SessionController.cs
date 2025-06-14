using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class SessionController(AppDbContext dbContext, IOverlappingService overlappingService) : ControllerBase
{
    [HttpGet("api/event/{eventId:guid}/sessions")]
    public async Task<IActionResult> GetSessions(Guid eventId)
    {
        var sessions = await dbContext.Session.Where(s => s.EventId == eventId).ToListAsync();
        var sessionDto = sessions.Select(x => new SessionDto
        {
            Id = x.Id,
            Name = x.Name,
            StartTime = x.StartTime,
            Duration = x.Duration,
        }
        ).ToList();
        return Ok(sessionDto);
    }

    [HttpGet("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> GetSingleSession(Guid sessionId)
    {
        var session = await dbContext.Session.FindAsync(sessionId);
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
        };
        return Ok(sessionDto);
    }

    [HttpPost("api/event/{eventId:guid}/sessions")]
    public async Task<IActionResult> CreateSession(Guid eventId, [FromBody] SessionDto newSession)
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
        return CreatedAtAction(nameof(GetSingleSession), new { sessionId = createdSession.Id }, createdSession);
    }

    [HttpPatch("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> UpdateSession(Guid sessionId, [FromBody] UpdateSessionDto updatedSession)
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

    [HttpDelete("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> DeleteSession(Guid sessionId)
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
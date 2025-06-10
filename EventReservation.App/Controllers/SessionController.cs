using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[ApiController]
//[Authorize(Roles = nameof(Roles.Admin))]
public class SessionController(AppDbContext dbContext) : ControllerBase
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
        var createdSessionStartTime = newSession.StartTime;
        var createdSessionEndTime = createdSessionStartTime.AddMinutes(newSession.Duration);
        //TODO: Sprawdzić poprawność działania
        if (!(newSession.StartTime >= eventFromDb.StartTime && createdSessionEndTime <= eventFromDb.EndTime))
        {
            return BadRequest("Invalid session time");
        }

        //TODO: Sprawdzić, poprawność działania 
        if (!eventFromDb.IsOverLappingAllowed && eventFromDb.Sessions != null)
        {
            var sessions = eventFromDb.Sessions;
            foreach (var session in sessions)
            {
                if(IsSessionOverlapping(session, newSession))
                {
                    return BadRequest("Session time overlaps with existing session");
                }
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
        //TODO: Zwrócić DTO zamiast encji
        return CreatedAtAction(nameof(GetSingleSession), new { sessionId = createdSession.Id }, createdSession);
    }

    [HttpPut("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> UpdateSession(Guid sessionId, [FromBody] UpdateSessionDto updatedSession)
    {
        var sessionFromDb = await dbContext.Session.FindAsync(sessionId);
        if (sessionFromDb == null)
        {
            return NotFound();
        }
        //TODO: Pamiętać o sprawdzaniu timestampa.
        //Cel: Unikanie 'podwójnego' aktualizowanai tej samej encji np. przez dwóch adminów w tym samym czasie 
        dbContext.Entry(sessionFromDb).CurrentValues.SetValues(updatedSession);
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

    private bool IsSessionOverlapping(Session session, SessionDto newSession)
    {
        var sessionStartTime = session.StartTime;
        var sessionEndTime = session.StartTime.AddMinutes(session.Duration);
        var newSessionStartTime = newSession.StartTime;
        var newSessionEndTime = newSession.StartTime.AddMinutes(newSession.Duration);
        return newSessionStartTime < sessionEndTime && newSessionEndTime > sessionStartTime;
    }
}
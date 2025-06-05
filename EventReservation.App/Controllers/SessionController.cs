using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.App.Controllers;

[ApiController]
public class SessionController(AppDbContext db) : ControllerBase
{
    [HttpGet("api/event/{eventId:guid}/sessions")]
    public async Task<IActionResult> GetSessionsAsync(Guid eventId)
    {
        var sessions = await db.Session.Where(s=> s.EventId == eventId).ToListAsync();
        //TODO: DTO
        return Ok(sessions);
    }
    [HttpGet("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> GetSingleSessionAsync(Guid sessionId)
    {
        var session = await db.Session.FindAsync(sessionId);
        if(session == null)
        {
            return NotFound();
        }
        //TODO: DTO
        return Ok(session);
    }

    [HttpPost("api/event/{eventId:guid}/sessions")]
    public async Task<IActionResult> CreateSessionAsync(Guid eventId, [FromBody] SessionDto newSession)
    {
        var eventFromDb = await db.Event.FindAsync(eventId);
        if(eventFromDb == null)
        {
            return BadRequest("No event");
        }
        //TODO: Walidacja czy data jest poprawan i czy mieści się w godzinach w których odbywa się event.
        //      Uwzględnić duration 
        var session = new Session
        {
            EventId = eventId,
            Name = newSession.Name,
            Description = newSession.Description,
            StartTime = newSession.StartTime,
            Duration = newSession.Duration,
        };
        await db.Session.AddAsync(session);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSingleSessionAsync), new { sessionId = session.Id }, session);

    }

    [HttpPut("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> UpdateSession(Guid sessionId, [FromBody] SessionDto newSession)
    {
        return Ok();
    }

    [HttpDelete("api/sessions/{sessionId:guid}")]
    public async Task<IActionResult> DeleteSession(Guid sessionId)
    {
        return Ok();
    }




}

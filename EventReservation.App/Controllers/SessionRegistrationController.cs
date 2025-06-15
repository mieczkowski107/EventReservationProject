using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using EventReservation.App.Services;
using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using EventReservation.Models.DTO.Session;
using EventReservation.Models.DTO.SessionRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace EventReservation.App.Controllers;

[ApiController]
//[Authorize(Roles = nameof(Roles.Client))]
public class SessionRegistrationController(AppDbContext dbContext, IOverlappingService overlap) : ControllerBase
{
    [HttpGet]
    [Route("api/registrations")]
    public async Task<IActionResult> GetUserRegistration()
    {
        var userId = UserService.GetUserId(User);
        var userRegistration = await dbContext.Registration.Include(r=>r.Session).Where(r => r.UserId == userId).ToListAsync();
        //TODO: DTO
        return Ok(userRegistration);
    }
    [HttpPost]
    [Route("api/session/{sessionId:int}/registrations")]
    public async Task<IActionResult> SessionSignIn(int sessionId)
    {
        var userId = UserService.GetUserId(User);

        //TODO: Sprawdź czy sesja nie minęła?
        //TODO: Rozwiąż sytuację wyścigu w zapisach - ale to zostawiam na pewno Tobie, bo już robiłeś to PUTach

        if (await dbContext.Registration.CountAsync(r => r.UserId == userId && r.SessionId == sessionId) > 0)
        {
            return BadRequest("User is already registered for this session.");
        }

        var session = await dbContext.Session
            .Include(s => s.Event)
            .SingleOrDefaultAsync(s => s.Id == sessionId);
        if (session == null) return NotFound("Session not found.");

        if (session.Event.IsOverLappingAllowed == true)
        {
            var existingSessions = await dbContext.Registration
                .Where(r => r.UserId == userId && r.Session.EventId == session.EventId)
                .Select(r => r.Session)
                .ToListAsync();
            if (overlap.AreSessionsOverlapping(existingSessions, session))  // korzystając z serwisu
            {
                return BadRequest("Session overlaps with existing sessions.");
            }
        }

        var limit = await dbContext.SessionLimit.SingleOrDefaultAsync(sl => sl.SessionId == sessionId);
        if (limit == null)
        {
            return BadRequest("Session limit not configured.");
        }
        if (limit.CurrentReserved >= limit.MaxParticipants)
        {
            return BadRequest("Session is full.");
        }

        var userRegistration = new Registration
        {
            SessionId = sessionId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Registration.Add(userRegistration);

        limit.CurrentReserved++;
        dbContext.SessionLimit.Update(limit);

        await dbContext.SaveChangesAsync();

        var sessionRegistrationDto = new SessionRegistrationDto
        {
            Id = userRegistration.Id,
            SessionId = userRegistration.SessionId,
            RegistrationStatus = userRegistration.RegistrationStatus,
            CreatedAt = userRegistration.CreatedAt
        };

        return Ok(sessionRegistrationDto);
    }
    [HttpDelete]
    [Route("api/session/{sessionId:int}/registrations")]
    public async Task<IActionResult> SessionSignOut(int sessionId)
    {
        var userId = UserService.GetUserId(User);

        var userRegistration = await dbContext.Registration.SingleOrDefaultAsync(u => u.UserId == userId &&  u.SessionId == sessionId);
        if (userRegistration == null) return NotFound("Registration not found.");

        var limit = await dbContext.SessionLimit.SingleOrDefaultAsync(sl => sl.SessionId == sessionId);
        limit.CurrentReserved--;
        dbContext.SessionLimit.Update(limit);

        dbContext.Remove(userRegistration);

        await dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    
}
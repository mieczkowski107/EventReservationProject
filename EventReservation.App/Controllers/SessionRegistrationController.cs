using System.Diagnostics;
using EventReservation.App.Services;
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
public class SessionRegistrationController(AppDbContext dbContext) : ControllerBase
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
        //TODO: Usunąć tego debuga później
        //foreach (var c in User.Claims)
        //{
        //    Debug.WriteLine($"Claim: Type = {c.Type}, Value = {c.Value}");
        //}
        var userId = UserService.GetUserId(User);
        //Debug.Assert(userId != null, "userId nie może być null");
        //Debug.WriteLine($"UserId: {userId}");

        //TODO: Sprawdź czy sesja nie minęła

        if (await dbContext.Registration.CountAsync(r => r.UserId == userId && r.SessionId == sessionId) > 0)
        {
            return BadRequest("User is already registered for this session.");
        }

        //TODO: testy - weryfikacja limitów
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
            UserId = (int)userId,       // mam wątpliwości co do tego sztywnego rzutowania, update: zrobiło się na szaro, ale nie pamiętam, żebym coś zmieniał XD
            RegistrationStatus = RegistrationStatus.Registered,
            CreatedAt = DateTime.UtcNow
        };
        dbContext.Registration.Add(userRegistration);

        limit.CurrentReserved++;
        dbContext.SessionLimit.Update(limit);

        await dbContext.SaveChangesAsync();

        //TODO: Czy DTO nie mogłoby być przekazane jako parametr?
        var sessionRegistrationDto = new SessionRegistrationDto
        {
            Id = userRegistration.Id,
            SessionId = userRegistration.SessionId,
            RegistrationStatus = userRegistration.RegistrationStatus,
            CreatedAt = userRegistration.CreatedAt
        };

        //TODO: Pamiętać przy overlapping allowed nie dopuścić do zarejestrowania tej samej osoby na inną sesję w tym samym czasie
        return Ok(sessionRegistrationDto);
    }
    [HttpDelete]
    [Route("api/session/{sessionId:int}/registrations")]
    public async Task<IActionResult> SessionSignOut(int sessionId)
    {
        var userId = UserService.GetUserId(User);
        var userRegistration =  dbContext.Registration.Where(u => u.UserId == userId &&  u.SessionId == sessionId);
        dbContext.Remove(userRegistration);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    
}
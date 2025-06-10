using EventReservation.App.Services;
using EventReservation.DataAccess;
using EventReservation.Models;
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
    [Route("api/session/{sessionId:guid}/registrations")]
    public async Task<IActionResult> SessionSignIn(Guid sessionId)
    {
        var userId = UserService.GetUserId(User);
        var userRegistration = await dbContext.Registration.Where(r => r.UserId == userId).ToListAsync();
        
        //TODO: Pamiętać przy overlapping allowed nie dopuścić do zarejestrowania tej samej osoby na inną sesję w tym samym czasie
        //      Sprawdzić czy użytkownik nie jest już zapisany na tą sesję i czy nie jest zapisany na inną sesję w tym samym czasie
        return Ok();
    }
    [HttpDelete]
    [Route("api/session/{sessionId:guid}/registrations")]
    public async Task<IActionResult> SessionSignOut(Guid sessionId)
    {
        var userId = UserService.GetUserId(User);
        var userRegistration =  dbContext.Registration.Where(u => u.UserId == userId &&  u.SessionId == sessionId);
        dbContext.Remove(userRegistration);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    
}
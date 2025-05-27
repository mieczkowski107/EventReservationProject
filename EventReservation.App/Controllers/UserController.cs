using System.Security.Claims;
using EventReservation.App.Services;
using EventReservation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace EventReservation.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenProvider tokenProvider) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!registerRequest.ArePasswordsEqual())
        {
            return BadRequest("Passwords do not match");
        }
        var user = new AppUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName!,
            LastName = registerRequest.LastName!,
            Country = registerRequest.Country,
            PhoneNumber = registerRequest.Phone
        };
        var result = await userManager.CreateAsync(user, registerRequest.Password!);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        if (User.IsInRole(Roles.Admin.ToString()))
        {
            await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
        }
        else
        {
            await userManager.AddToRoleAsync(user, Roles.Client.ToString());
        }
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await userManager.FindByNameAsync(model.Login!);
        if (user == null)
        {
            return BadRequest();
        }
        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password!, false);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        var token = tokenProvider.Create(user);
        return Ok(token);
    }

    [HttpGet("logout")]
    public  IActionResult Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok();
    }
    
    //TODO: Refresh tokens
}
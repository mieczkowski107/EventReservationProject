using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventReservation.App.Services;
using EventReservation.Models;
using EventReservation.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace EventReservation.App.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    TokenProvider tokenProvider) : ControllerBase
{

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var profile = new UserInfo
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Country = user.Country,
            Phone = user.PhoneNumber
        };
        return Ok(profile);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var userExists = await userManager.FindByEmailAsync(registerRequest.Email!);
        if (userExists != null)
        {
            return BadRequest("User already exists with this email");
        }

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

        await userManager.AddToRoleAsync(user, nameof(Roles.Client));
        return Ok();
    }

    [HttpPost("register-admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest registerRequest)
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

        await userManager.AddToRoleAsync(user, nameof(Roles.Admin));

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await userManager.FindByEmailAsync(model.Email!);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password!, false);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        var roles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        };
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        authClaims.AddRange(roleClaims);
        var claimsIdentity = new ClaimsIdentity(authClaims, "Token", JwtRegisteredClaimNames.Sub, ClaimTypes.Role);

        var token = tokenProvider.Create(claimsIdentity);
        return Ok(token);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok();
    }
}

  
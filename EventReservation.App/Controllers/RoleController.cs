using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventReservation.App.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class RoleController(RoleManager<IdentityRole> roleManager) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
    {
        return await Task.FromResult(roleManager.Roles.ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IdentityRole>> GetRoleById(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return BadRequest();
        }
        return await Task.FromResult(role);
    }

    [HttpPost]
    public async Task<ActionResult<IdentityRole>> CreateRole(string name)
    {
        if (await roleManager.FindByNameAsync(name) != null)
        {
            return BadRequest();
        }
        var newRole = new IdentityRole(name);
        await roleManager.CreateAsync(newRole);
        return await Task.FromResult(newRole);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IdentityRole>> UpdateRole(string id, string name)
    {
        var role = await roleManager.FindByIdAsync(id);
        if(role == null) return BadRequest();
        await roleManager.UpdateAsync(role);
        return await Task.FromResult(role);
    }

    [HttpDelete]
    public async Task<ActionResult<IdentityRole>> DeleteRole(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if(role == null) return BadRequest();
        await roleManager.DeleteAsync(role);
        return await Task.FromResult(role);
    }
    
    
    
}
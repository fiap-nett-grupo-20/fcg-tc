using FCG.Application.DTO;
using FCG.Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FCG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController : ApiBaseController
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RolesController(
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IdentityRole>> GetRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<IdentityRole>> CreateRole([FromBody] CreateRoleModel model)
    {
        if (await _roleManager.RoleExistsAsync(model.Name))
            return BadRequest(new { Message = "Role já existe." });

        var role = new IdentityRole(model.Name);
        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);

        return BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleModel model)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            return NotFound();

        var existingRole = await _roleManager.FindByNameAsync(model.Name);
        if (existingRole != null && existingRole.Id != id)
            return BadRequest(new { Message = "Outra role com este nome já existe." });

        role.Name = model.Name;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
            return Success("Role atualizada com sucesso!");

        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            return NotFound();

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
            return NoContent();

        return BadRequest(result.Errors);
    }

    [HttpPost("add-role-to-user")]
    public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound(new { Message = "Usuário não encontrado." });

        if (!await _roleManager.RoleExistsAsync(model.Role))
            return NotFound(new { Message = "Role não encontrada." });

        var result = await _userManager.AddToRoleAsync(user, model.Role);

        if (result.Succeeded)
            return Ok(new { Message = "Role atribuída ao usuário com sucesso." });

        return BadRequest(result.Errors);
    }

    [HttpPost("remove-role-from-user")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] UserRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound(new { Message = "Usuário não encontrado." });

        if (!await _roleManager.RoleExistsAsync(model.Role))
            return NotFound(new { Message = "Role não encontrada." });

        var result = await _userManager.RemoveFromRoleAsync(user, model.Role);

        if (result.Succeeded)
            return Ok(new { Message = "Role removida do usuário com sucesso." });

        return BadRequest(result.Errors);
    }
}
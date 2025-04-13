using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Roles = "Admin")] 
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // GET: api/Roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles); 
    }

    // GET: api/Roles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<IdentityRole>> GetRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    // POST: api/Roles
    public class CreateRoleModel
    {
        public required string Name { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<IdentityRole>> CreateRole(CreateRoleModel model)
    {
        if (await _roleManager.RoleExistsAsync(model.Name))
        {
            ModelState.AddModelError(string.Empty, "Role já existe.");
            return BadRequest(ModelState);
        }

        var role = new IdentityRole(model.Name);
        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
    }

    // DELETE: api/Roles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        // Cuidado: sempre verificar se a role está em uso antes de excluir,
        // Ex: var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
        // if (usersInRole.Any()) { return BadRequest("Role está em uso."); }


        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return NoContent(); // Sucesso
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState); // Ou InternalServerError
        }
    }

    // PUT api/Roles/{id} - Atualizar nome (menos comum, mas possível)
    public class UpdateRoleModel
    {
        public required string Name { get; set; }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, UpdateRoleModel model)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        // Verifica se o novo nome já existe (exceto para a própria role sendo atualizada)
        var existingRole = await _roleManager.FindByNameAsync(model.Name);
        if (existingRole != null && existingRole.Id != id)
        {
            ModelState.AddModelError(string.Empty, "Outra role com este nome já existe.");
            return BadRequest(ModelState);
        }

        role.Name = model.Name;
        // role.NormalizedName = _roleManager.NormalizeKey(model.Name); // O UpdateAsync deve cuidar disso

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
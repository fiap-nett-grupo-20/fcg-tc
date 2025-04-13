using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para ToListAsync() se necessário buscar todos os usuários

[Route("api/[controller]")]
[ApiController]
// [Authorize] // para usar com o jwt autorização conforme necessário (ex: [Authorize(Roles = "Admin")])
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager; // Injetar para caso se precisar lidar com roles aqui

    // Injete os managers necessários
    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // --- Operações de Usuário ---

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
    {
        // Cuidado: Retornar todos os usuários pode expor dados sensíveis
        // Considere retornar um DTO (Data Transfer Object) com campos limitados.
        var users = await _userManager.Users.ToListAsync();
        return Ok(users); // Idealmente, mapeie para um UserDTO
    }

    // GET: api/Users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<IdentityUser>> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user); // Idealmente, mapeie para um UserDTO
    }

    // POST: api/Users
    // Modelo para criação 
    public class CreateUserModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public List<string>? Roles { get; set; } // Opcional: Roles a serem atribuídas
    }

    [HttpPost]
    public async Task<ActionResult<IdentityUser>> CreateUser(CreateUserModel model)
    {
        var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Adicionar roles, se fornecidas
            if (model.Roles != null && model.Roles.Any())
            {
                // Verifique se as roles existem antes de adicionar
                foreach (var roleName in model.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _userManager.AddToRoleAsync(user, roleName);
                    }
                    else
                    {
                        // Log ou retorne um erro informando que a role não existe
                        Console.WriteLine($"Atenção: Role '{roleName}' não encontrada ao criar usuário '{user.UserName}'.");
                    }
                }
            }

            // Retorna o usuário criado (sem a senha, obviamente)
            // Pode ser útil retornar o ID ou o objeto completo (mapeado para DTO)
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user); // Idealmente, mapeie para um UserDTO
        }
        else
        {
            // Retorna os erros de validação do Identity
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
    }

    // PUT: api/Users/{id} - Exemplo para atualizar email (outras atualizações podem precisar de métodos específicos)
    public class UpdateUserModel
    {
        public required string Email { get; set; }
        // Adicione outros campos que podem ser atualizados diretamente
        public string? PhoneNumber { get; set; }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        // Cuidado ao atualizar UserName, pode ter implicações
        // user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return NoContent(); // Sucesso, sem conteúdo para retornar
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


    // DELETE: api/Users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

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
            return BadRequest(ModelState); // Ou InternalServerError dependendo do erro
        }
    }

    // --- Operações de Roles do Usuário ---

    // POST: api/Users/{userId}/roles
    public class UserRoleModel
    {
        public required string RoleName { get; set; }
    }

    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddRoleToUser(string userId, UserRoleModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        if (!await _roleManager.RoleExistsAsync(model.RoleName))
        {
            return NotFound("Role não encontrada.");
        }

        // Verifica se o usuário já tem a role para evitar erro
        if (await _userManager.IsInRoleAsync(user, model.RoleName))
        {
            return BadRequest("Usuário já possui esta role.");
        }

        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
        if (result.Succeeded)
        {
            return Ok();
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

    // DELETE: api/Users/{userId}/roles/{roleName}
    // Nota: Usamos UrlEncode para o nome da role se ele puder conter caracteres especiais
    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
    {
        // string decodedRoleName = System.Net.WebUtility.UrlDecode(roleName); // Descodifique se necessário

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        if (!await _roleManager.RoleExistsAsync(roleName)) // Use decodedRoleName se descodificou
        {
            return NotFound("Role não encontrada.");
        }

        // Verifica se o usuário realmente tem a role
        if (!await _userManager.IsInRoleAsync(user, roleName)) // Use decodedRoleName
        {
            return BadRequest("Usuário não possui esta role.");
        }


        var result = await _userManager.RemoveFromRoleAsync(user, roleName); // Use decodedRoleName

        if (result.Succeeded)
        {
            return Ok();
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

    // GET: api/Users/{userId}/roles
    [HttpGet("{userId}/roles")]
    public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }
}
using FGC.API.DTO;
using FGC.API.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public UsersController(
    IUserService userService,
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    JwtService jwtService)
    {
        _userService = userService;
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized("Usuário ou senha inválidos.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);
        if (!result.Succeeded)
            return Unauthorized("Usuário ou senha inválidos.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user, roles); 

        return Ok(new
        {
            Token = token,
            Usuario = new { user.Id, user.Email, Roles = roles }
        });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserModel model)
    {
        var user = await _userService.CreateUserAsync(model);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserModel model)
    {
        await _userService.UpdateUserAsync(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}

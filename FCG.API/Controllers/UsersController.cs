using FCG.API.Controllers;
using FCG.Application;
using FCG.Application.DTO;
using FCG.Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ApiBaseController
{
    private readonly IUserService _userService;

    public UsersController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Success(users, "Lista de usuários retornada com sucesso.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Success(user, "Usuário encontrado.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserModel model)
    {
        var user = await _userService.CreateUserAsync(model);
        return CreatedResponse(user, "Usuário criado com sucesso.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserModel model)
    {
        await _userService.UpdateUserAsync(id, model);
        return Success("Usuário atualizado com sucesso.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        return Success("Usuário deletado com sucesso.");
    }
}

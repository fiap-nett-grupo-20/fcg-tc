using FCG.Application.DTO;
using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            NameUser = user.Name,
            Email = user.Email.Value,
            Roles = _userManager.GetRolesAsync(user).Result.ToList()
        });
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new BusinessErrorDetailsException("Usuário não encontrado.");

        var roles = await _userManager.GetRolesAsync(user);
        return new UserDto
        {
            Id = user.Id,
            NameUser = user.Name,
            Email = user.Email.Value,
            Roles = roles.ToList()
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserModel model)
    {
        var user = new User
        (
            model.NameUser,
            model.Email,
            model.Password
        );

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new BusinessErrorDetailsException("Erro ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        if (model.Roles != null && model.Roles.Any())
        {
            foreach (var roleName in model.Roles)
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                    await _userManager.AddToRoleAsync(user, roleName);
                else
                    throw new BusinessErrorDetailsException($"Role '{roleName}' não encontrada.");
            }
        }

        var roles = await _userManager.GetRolesAsync(user);
        return new UserDto
        {
            Id = user.Id,
            NameUser = user.Name,
            Email = user.Email.Value,
            Roles = roles.ToList()
        };
    }

    public async Task UpdateUserAsync(string id, UpdateUserModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new BusinessErrorDetailsException("Usuário não encontrado.");

        if (!string.IsNullOrWhiteSpace(model.NameUser))
            user.Name = model.NameUser;

        if (!string.IsNullOrWhiteSpace(model.Email))
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
            if (!setEmailResult.Succeeded)
                throw new BusinessErrorDetailsException("Erro ao atualizar email: " + string.Join(", ", setEmailResult.Errors.Select(e => e.Description)));
        }

        if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            if (!setPhoneResult.Succeeded)
                throw new BusinessErrorDetailsException("Erro ao atualizar telefone: " + string.Join(", ", setPhoneResult.Errors.Select(e => e.Description)));
        }

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new BusinessErrorDetailsException("Erro ao atualizar usuário: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new BusinessErrorDetailsException("Usuário não encontrado.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new BusinessErrorDetailsException("Erro ao excluir usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}

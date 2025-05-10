using FGC.API.DTO;
using FGC.API.Models;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserDto> CreateUserAsync(CreateUserModel model);
    Task UpdateUserAsync(string id, UpdateUserModel model);
    Task DeleteUserAsync(string id);
}

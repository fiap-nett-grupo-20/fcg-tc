using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string id);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(string id);
    Task<bool> ExistsByEmailAsync(string email);
}

using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(int id);
    Task<IEnumerable<Game>> GetAllAsync();
    Task<bool> TitleExistsAsync(string title);
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(int id);
}

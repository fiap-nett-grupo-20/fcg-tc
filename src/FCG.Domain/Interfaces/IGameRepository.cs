using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IGameRepository
{
    Task<Game> GetByIdAsync(string id);
    Task<IEnumerable<Game>> GetAllAsync();
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(string id);
}

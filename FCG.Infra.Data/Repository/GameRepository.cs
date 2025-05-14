using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Repository;

public class GameRepository(FCGDbContext context) : IGameRepository
{
    private readonly FCGDbContext _context = context;
    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }


    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Game> GetByIdAsync(int id)
    {
        return await _context.Games
            .FindAsync(id)
            ?? throw new Exception("Jogo não encontrado.");
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }
}

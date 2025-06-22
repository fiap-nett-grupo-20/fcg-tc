using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Repository;

public class GameRepository(FCGDbContext context) : IGameRepository
{
    private readonly FCGDbContext _context = context;
    private const string CaseAndAccentInsensitive = "SQL_Latin1_General_CP1_CI_AI"; 
    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var game = await GetByIdAsync(id);
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return
            await _context.Games.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Game?> GetByTitleAsync(string title)
    {
            return await _context.Games
                .FirstOrDefaultAsync(g => EF.Functions.Collate(g.Title, CaseAndAccentInsensitive) ==
                                         EF.Functions.Collate(title, CaseAndAccentInsensitive));
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }
}

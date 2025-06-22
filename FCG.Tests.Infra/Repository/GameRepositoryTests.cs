using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Infra.Data.Context;
using FCG.Infra.Data.Repository;
using FCG.Domain.ValueObjects;
using FCG.Domain.Exceptions;

namespace FCG.Infra.Tests.Repositories;

public class GameRepositoryTests : IDisposable
{
    private readonly FCGDbContext _context;
    private readonly GameRepository _repository;

    public GameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FCGTestDB;Trusted_Connection=True;")
            .Options;
        _context = new FCGDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _repository = new GameRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ValidGame_ShouldAddGameToDatabase()
    {
        // Arrange
        var game = new Game("Game Title", 199.99M, "descricao", "terror");

        // Act
        await _repository.AddAsync(game);

        // Assert
        var savedGame = await _context.Games.FirstOrDefaultAsync();
        Assert.NotNull(savedGame);
        Assert.Equal("Game Title", savedGame.Title);
    }

    [Fact]
    public async Task AddAsync_SameGameTitle_ThrowException()
    {
        // Arrange
        var game = new Game("Game Title", 199.99M, "descricao", "terror");
        await _repository.AddAsync(game);

        var sameGameTitle = new Game("Game Title", 99.99M, "descrição", "casual");

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () => await _repository.AddAsync(sameGameTitle));
    }

    [Fact]
    public async Task UpdateAsync_UpdateTitleThatAlreadyExists_ThrowException()
    {
        // Arrange
        var existingGame = new Game("Game Title", 199.99M, "descricao", "terror");
        await _repository.AddAsync(existingGame);

        var game = new Game("New Game Title", 199.99M, "descricao", "terror");
        await _repository.AddAsync(game);

        game.Title = "Game Title";

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () => await _repository.UpdateAsync(game));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

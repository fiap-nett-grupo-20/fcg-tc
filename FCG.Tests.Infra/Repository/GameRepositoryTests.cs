using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Infra.Data.Context;
using FCG.Infra.Data.Repository;
using FCG.Domain.ValueObjects;

namespace FCG.Infra.Tests.Repositories;

public class GameRepositoryTests
{
    private readonly FCGDbContext _context;
    private readonly GameRepository _repository;

    public GameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new FCGDbContext(options);
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
}

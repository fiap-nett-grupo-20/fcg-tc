using Microsoft.EntityFrameworkCore;
using FCG.Domain.ValueObjects;
using FCG.Domain.Entities;
using FCG.Infra.Data.Context;
using FCG.Infra.Data.Repository;

namespace FCG.Infra.Tests.Repositories;

public class UserRepositoryTests
{
    private readonly FCGDbContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new FCGDbContext(options);
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ValidUser_ShouldAddUserToDatabase()
    {
        //  Arrange
        var user = new User("José Silva", "rm000000@fiap.com.br", "Senha@123");

        // Act
        await _repository.AddAsync(user);

        // Assert
        var saveUser = await _context.Users.FirstOrDefaultAsync();
        Assert.NotNull(saveUser);
        Assert.Equal("rm000000@fiap.com.br", saveUser.Email!.Address);
    }

    [Fact]
    public async Task DeleteAsync_ExistUser_ShouldDeleteUserFromDatabase()
    {
        // Arrange
        var user = new User("José Silva", "rm000000@fiap.com.br", "Senha@123");

        // Act
        await _repository.AddAsync(user);
        await _repository.DeleteAsync(user.Id!);

        // Assert
        Assert.Null(await _context.Users.FindAsync(user.Id));
    }

    [Fact]
    public async Task UpdateAsync_ExistUser_ShouldUpdateUser()
    {
        // Arrange
        var user = new User("José Silva", new Email("rm000000@fiap.com.br"), new Password("Senha@123"));

        // Act
        await _repository.AddAsync(user);
        user.Email = new Email("professor@pm3.com.br");
        await _repository.UpdateAsync(user);

        // Assert
        Assert.True(await _repository.ExistsByEmailAsync("professor@pm3.com.br"));
    }
}

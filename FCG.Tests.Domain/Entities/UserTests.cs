using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.ValueObjects;

namespace FCG.Tests.Domain.Entities;

public class UserTests
{
    // --- Testes para Nome ---
    [Fact]
    public void Constructor_ValidName_CreatesUser()
    {
        // Arrange & Act
        var password = new Password("Senha@123");
        var user = new User("José Silva", "rm000000@fiap.com.br", password);

        // Assert
        Assert.Equal("José Silva", user.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsException(string invalidName)
    {
        // Arrange, Act & Assert
        var password = new Password("Senha@123");
        Assert.Throws<ArgumentException>(() => new User(invalidName, "rm000000@fiap.com.br", password));
    }

    // --- Testes para Id e Role ---
    [Fact]
    public void Constructor_DefaultRole_IsUser()
    {
        // Arrange & Act
        var password = new Password("Senha@123");
        var user = new User("José", "rm000000@fiap.com.br", password);

        // Assert
        Assert.Equal(UserRole.User, user.Role);
    }

    [Fact]
    public void Constructor_AdminRole_SetsRoleCorrectly()
    {
        // Arrange & Act
        var password = new Password("Senha@123");
        var user = new User("Admin", "admin@fiap.com.br", password, UserRole.Admin);

        // Assert
        Assert.Equal(UserRole.Admin, user.Role);
    }

    // --- Testes para Email (via VO) ---
    [Fact]
    public void Constructor_ValidEmailVO_CreatesUser()
    {
        // Arrange & Act
        var password = new Password("Senha@123");
        var user = new User("José Silva", "aluno@fiap.com.br", password);
        

        // Assert
        Assert.Equal("aluno@fiap.com.br", user.Email!.Address);
    }

    // --- Testes para Password (via VO) ---
    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = new Password("Pa$$w0rd");
        var user = new User("José Silva", "rm000000@fiap.com.br", password);

        //Act
        var result = user.Password.Verify("Pa$$w0rd");

        // Assert
        Assert.True(result);
    }
}
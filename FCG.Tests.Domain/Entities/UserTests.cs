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
        var user = new User("José Silva", "rm000000@fiap.com.br", "Senha@123");

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
        Assert.Throws<ArgumentException>(() => new User(invalidName, "rm000000@fiap.com.br", "Senha@123"));
    }

    // --- Testes para Id e Role ---
    [Fact]
    public void Constructor_DefaultRole_IsUser()
    {
        // Arrange & Act
        var user = new User("José", "rm000000@fiap.com.br", "Senha@123");

        // Assert
        Assert.Equal(UserRole.User, user.Role);
    }

    [Fact]
    public void Constructor_AdminRole_SetsRoleCorrectly()
    {
        // Arrange & Act
        var user = new User("Admin", "admin@fiap.com.br", "Senha@123", UserRole.Admin);

        // Assert
        Assert.Equal(UserRole.Admin, user.Role);
    }

    // --- Testes para Email (via VO) ---
    [Fact]
    public void Constructor_ValidEmailVO_CreatesUser()
    {
        // Arrange & Act
        var user = new User("José Silva", "aluno@fiap.com.br", "Senha@123");

        // Assert
        Assert.Equal("aluno@fiap.com.br", user.Email!.Address);
    }

    // --- Testes para Password (via VO) ---
    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange & Act
        var user = new User("José Silva", "rm000000@fiap.com.br", "Pa$$w0rd");

        // Assert
        Assert.True(user.VerifyPassword("Pa$$w0rd"));
    }
}
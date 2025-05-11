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
        // Arrange
        var email = new Email("rm000000@fiap.com.br");
        var password = new Password("Senha@123");

        // Act
        var user = new User("José Silva", email, password);

        // Assert
        Assert.Equal("José Silva", user.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsException(string invalidName)
    {
        // Arrange
        var email = new Email("rm000000@fiap.com.br");
        var password = new Password("Senha@123");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new User(invalidName, email, password));
    }

    // --- Testes para Id e Role ---
    [Fact]
    public void Constructor_DefaultRole_IsUser()
    {
        var user = new User("José", new Email("rm000000@fiap.com.br"), new Password("Senha@123"));
        Assert.Equal(UserRole.User, user.Role);
    }

    [Fact]
    public void Constructor_AdminRole_SetsRoleCorrectly()
    {
        var user = new User("Admin", new Email("admin@fiap.com.br"), new Password("Senha@123"), UserRole.Admin);
        Assert.Equal(UserRole.Admin, user.Role);
    }

    // --- Testes para Email (via VO) ---
    [Fact]
    public void Constructor_ValidEmailVO_CreatesUser()
    {
        // Arrange
        var validEmail = new Email("aluno@fiap.com.br");
        var password = new Password("Senha@123");

        // Act
        var user = new User("José Silva", validEmail, password);

        // Assert
        Assert.Equal(validEmail, user.Email);
    }

    // --- Testes para Password (via VO) ---
    [Fact]
    public void Constructor_ValidPasswordVO_CreatesUser()
    {
        // Arrange
        var email = new Email("rm000000@fiap.com.br");
        var validPassword = new Password("Pa$$w0rd");

        // Act
        var user = new User("José Silva", email, validPassword);

        // Assert
        Assert.Equal(validPassword, user.Password);
    }

    // --- Teste para verificação de senha ---
    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var email = new Email("rm000000@fiap.com.br");
        var password = new Password("Senha@123");
        var user = new User("José Silva", email, password);

        // Act & Assert
        Assert.True(user.VerifyPassword("Senha@123")); // Método na entidade User
    }
}
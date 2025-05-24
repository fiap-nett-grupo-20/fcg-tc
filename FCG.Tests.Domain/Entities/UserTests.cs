using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.ValueObjects;

using Microsoft.AspNetCore.Identity;

namespace FCG.Tests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ValidName_CreatesUser()
    {
        // Arrange & Act
        var password = new Password("Senha@123");
        var user = new User("José Silva", "rm000000@fiap.com.br");

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
        Assert.Throws<ArgumentException>(() => new User(invalidName, "rm000000@fiap.com.br"));
    }

    [Fact]
    public void Constructor_DefaultRole_IsUser()
    {
        // Arrange & Act
        var user = new User("José", "rm000000@fiap.com.br");

        // Assert
        Assert.Equal(UserRole.User, user.Role);
    }

    [Fact]
    public void Constructor_AdminRole_SetsRoleCorrectly()
    {
        // Arrange & Act
        var user = new User("Admin", "admin@fiap.com.br", UserRole.Admin);

        // Assert
        Assert.Equal(UserRole.Admin, user.Role);
    }

    [Fact]
    public void Constructor_ValidEmailVO_CreatesUser()
    {
        // Arrange & Act
        var user = new User("José Silva", "aluno@fiap.com.br");
        

        // Assert
        Assert.Equal("aluno@fiap.com.br", user.Email!);
    }
}
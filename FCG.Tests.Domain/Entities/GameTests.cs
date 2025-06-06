﻿using FCG.Domain.Entities;
using FCG.Domain.Exceptions;

namespace FCG.Tests.Domain.Entities;

public class GameTests
{
    [Fact]
    public void Constructor_ValidTitle_CreatesGame()
    {
        //Arrange / Act
        var game = new Game("Test Game", 0.99m, "descricao", "terror");

        // Assert
        Assert.Equal("Test Game", game.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidTitle_ThrowsBusinessErrorDetailsException(string invalidTitle)
    {
        Assert.Throws<BusinessErrorDetailsException>(() => new Game(invalidTitle, 0.99m, "descricao", "terror"));
    }

    [Fact]
    public void Constructor_ValidPrice_CreatesGame()
    {
        // Arrange & Act
        var game = new Game("Test Game", 0.99m, "descricao", "terror");

        // Assert
        Assert.Equal(0.99m, game.Price);
    }
}

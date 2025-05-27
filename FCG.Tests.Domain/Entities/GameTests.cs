using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;

namespace FCG.Tests.Domain.Entities;

public class GameTests
{
    [Fact]
    public void Constructor_ValidTitle_CreatesGame()
    {
        // Arrange
        Price price = new Price(0.99m);

        //Act
        var game = new Game("Test Game", price, "descricao", "terror");

        // Assert
        Assert.Equal("Test Game", game.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidTitle_ThrowsBusinessErrorDetailsException(string invalidTitle)
    {
        // Arrange
        Price price = new Price(0.99m);

        // Act & Assert
        Assert.Throws<BusinessErrorDetailsException>(() => new Game(invalidTitle, price, "descricao", "terror"));
    }

    [Fact]
    public void Constructor_ValidPrice_CreatesGame()
    {
        // Arrange
        Price price = new Price(0.99m);

        // Act
        var game = new Game("Test Game", price, "descricao", "terror");

        // Assert
        Assert.Equal(0.99m, game.Price);
    }

    [Fact]
    public void Constructor_InvalidPrice_ThrowsBusinessErrorDetailsException()
    {
        // Arrange
        Price price = new Price(-10m);

        // Act & Assert
        Assert.Throws<BusinessErrorDetailsException>(() => new Price(-10m));
    }
}

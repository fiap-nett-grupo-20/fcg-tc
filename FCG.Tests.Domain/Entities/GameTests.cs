using FCG.Domain.Entities;
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
        var game = new Game("Test Game", price, "", "");

        // Assert
        Assert.Equal("Test Game", game.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void COnstructor_InvalidTitle_ThrowsArgumentException(string invalidTitle)
    {
        // Arrange
        Price price = new Price(0.99m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Game(invalidTitle, price, "", ""));
    }

    [Fact]
    public void Constructor_ValidPrice_CreatesGame()
    {
        // Arrange
        Price price = new Price(0.99m);

        // Act
        var game = new Game("Test Game", price, "", "");

        // Assert
        Assert.Equal(0.99m, game.Price);
    }

    [Fact]
    public void Constructor_InvalidPrice_ThrowsArgumentException()
    {
        // Arrange
        Price price = new Price(-10m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Game("Test Game", price, "", ""));
    }

    [Fact]
    public void Constructor_GeneratesIdAutomatically()
    {
        // Arrange
        Price price = new Price(10.99m);

        var game = new Game("Título", price, "Descrição", "Ação");
        Assert.NotNull(game.Id);
        //Assert.NotEmpty(game.Id);
    }
}

namespace FCG.Domain.Entities;

public class Game
{
    public string? Id { get; private set; }
    public string? Title { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
    public string? Genre { get; private set; }

    public Game(string title, decimal price, string description, string genre)
    {
        ValidateTitle(title);
        ValidatePrice(price);
        Id = Guid.NewGuid().ToString();
        Title = title;
        Price = price;
        Description = description;
        Genre = genre;
    }

    protected Game() { } // For EF Core

    private static void ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Preço não pode ser negativo.", nameof(price));
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título não pode ser vazio ou nulo.", nameof(title));
    }
}

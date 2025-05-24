namespace FCG.Domain.Entities;

public class Game
{
    public int Id { get;  set; }
    public string? Title { get;  set; }
    public decimal Price { get;  set; }
    public string? Description { get;  set; }
    public string? Genre { get;  set; }

    public Game(string title, decimal price, string description, string genre)
    {
        ValidateTitle(title);
        ValidatePrice(price);
        Title = title;
        Price = price;
        Description = description;
        Genre = genre;
    }

    public Game(int id, string title, decimal price, string description, string genre)
    {
        ValidateTitle(title);
        ValidatePrice(price);
        Id = id;
        Title = title;
        Price = price;
        Description = description;
        Genre = genre;
    }

    public Game() { } // For EF Core

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

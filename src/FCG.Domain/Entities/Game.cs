namespace FCG.Domain.Entities;

public class Game
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public Game()
    {
        
    }

    public Game(string title, decimal price, string description, string genre)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título não pode ser vazio ou nulo.", nameof(title));
        if (price < 0)
            throw new ArgumentException("Preço não pode ser negativo.", nameof(price));

        Title = title;
        Price = price;
        Description = description;
        Genre = genre;
    }
}

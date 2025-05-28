using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class Game
{
    public int Id { get;  set; }
    public string? Title { get;  set; }
    public decimal Price { get;  set; }
    public string? Description { get;  set; }
    public string? Genre { get;  set; }

    
    public Game(int id, string title, decimal price, string description, string genre)
        : this(title, price, description, genre)
    {
        Id = id;
    }

    public Game(string title, decimal price, string description, string genre)
    {
        ValidateTitle(title);
        ValidatePrice(price);
        ValidateDescription(description);
        ValidateGenre(genre);

        Title = title;
        Price = price;
        Description = description;
        Genre = genre;
    }

    public Game() { } // For EF Core
   

    public static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new BusinessErrorDetailsException("Título não pode ser vazio ou nulo.");
    }

    public static void ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new BusinessErrorDetailsException("Preço não pode ser negativo.");
    }

    public static void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessErrorDetailsException("Descrição não pode ser vazio ou nulo.");
    }

    public static void ValidateGenre(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
            throw new BusinessErrorDetailsException("Genero não pode ser vazio ou nulo.");
    }
}

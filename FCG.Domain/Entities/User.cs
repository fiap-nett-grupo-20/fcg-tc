using FCG.Domain.Enums;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class User
{
    public string? Id { get; private set; }
    public string? Name { get; set; }
    public Email? Email { get; set; }
    public Password? Password { get; set; }
    public UserRole Role { get; set; }
    public User(string name, string email, string password, UserRole role = UserRole.User)
    {
        ValidateName(name);
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = new Email(email);
        Password = new Password(password);
        Role = role;
    }

    public User(string name, Email email, Password password, UserRole role = UserRole.User)
    {
        ValidateName(name);
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    protected User() { } // For EF Core

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");
    }

    public bool VerifyPassword(string plainTextPassword) => BCrypt.Net.BCrypt.Verify(plainTextPassword, Password.Hash);
}

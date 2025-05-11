using FCG.Domain.Enums;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class User
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserRole Role { get; private set; }
    public User(string name, Email email, Password password, UserRole role = UserRole.User)
    {
        ValidateName(name);
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");
    }

    public bool VerifyPassword(string plainTextPassword) => BCrypt.Net.BCrypt.Verify(plainTextPassword, Password.Hash);
}

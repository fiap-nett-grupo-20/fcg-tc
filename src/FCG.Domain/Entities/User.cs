using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class User
{

    public string Name { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public User(string name, Email email, Password password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");

        Name = name;
        Email = email;
        Password = password;
    }

    public bool VerifyPassword(string plainTextPassword) => BCrypt.Net.BCrypt.Verify(plainTextPassword, Password.Hash);
}

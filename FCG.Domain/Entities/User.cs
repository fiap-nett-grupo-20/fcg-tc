using FCG.Domain.Enums;
using FCG.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace FCG.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    public User(string name, string email, UserRole role = UserRole.User)
    {
        ValidateName(name);
        var emailAddress = new Email(email);

        Name = name;
        Email = emailAddress.Address;
        UserName = emailAddress.Address; // Identity exige isso
        Role = role;
    }

    protected User() { }
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");
    }
}

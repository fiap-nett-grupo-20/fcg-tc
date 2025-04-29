using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

public record Password
{
    public string Hash { get; }
    public Password(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Senha não pode ser vazia");
        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Senha deve ter pelo menos 8 caracteres");
        if (!Regex.IsMatch(plainTextPassword, "[a-zA-Z]") ||
            !Regex.IsMatch(plainTextPassword, "[0-9]") ||
            !Regex.IsMatch(plainTextPassword, "[^a-zA-Z0-9]"))
            throw new ArgumentException("Senha deve conter letras, números e símbolos.", nameof(plainTextPassword));

        Hash = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
    }
}

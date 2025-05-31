using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

public class Password
{
    public string Hash { get; }
    public string PlainText { get; private set; }

    public Password(string plainTextPassword)
    {
        Validate(plainTextPassword);
        PlainText = plainTextPassword;
        Hash = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
    }

    protected Password() { }

    /// <summary>
    /// Verifica se a senha em texto puro corresponde ao hash armazenado.
    /// </summary>
    public bool Verify(string plainTextPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, Hash);
    }

    /// <summary>
    /// Validação interna da senha no momento da criação.
    /// </summary>
    private static void Validate(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Senha não pode ser vazia.");

        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

        if (!Regex.IsMatch(plainTextPassword, "[a-zA-Z]"))
            throw new ArgumentException("Senha deve conter pelo menos uma letra.");

        if (!Regex.IsMatch(plainTextPassword, "[0-9]"))
            throw new ArgumentException("Senha deve conter pelo menos um número.");

        if (!Regex.IsMatch(plainTextPassword, "[^a-zA-Z0-9]"))
            throw new ArgumentException("Senha deve conter pelo menos um símbolo.");
    }
}
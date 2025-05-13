using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

public record Email
{
    public string? Address { get; }
    public Email(string value)
    {
        ValidateEmail(value);
        ValidateEmailDomain(value);
        Address = value;
    }

    protected Email() { } // For EF Core

    private static void ValidateEmailDomain(string value)
    {
        if (!Regex.IsMatch(value, @"@(fiap\.com\.br|pm3\.com\.br|alura\.com\.br)$"))
            throw new ArgumentException("Email deve pertencer aos domínios @fiap.com.br, @pm3.com.br ou @alura.com.br.", nameof(value));
    }

    private static void ValidateEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email não pode ser vazio.", nameof(value));
    }

    public override string ToString() => Address!;
}
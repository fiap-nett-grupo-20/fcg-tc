using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

public record Email
{
    public string Value { get; set; }
    public Email(string value)
    {
        if (!Regex.IsMatch(value, @"@(fiap\.com\.br|pm3\.com\.br|alura\.com\.br)$"))
            throw new ArgumentException("Email inválido");

        Value = value;
    }
}

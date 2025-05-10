using FCG.Domain.ValueObjects;

namespace FCG.Tests.Domain.ValueObjects;

public class PasswordTests
{
    [Theory]
    [InlineData("Senha@1234")]
    [InlineData("Pa$$w0rd")]
    public void Constructor_ValidPassword_CreatesInstance(string validPassword)
    {
        var password = new Password(validPassword);
        Assert.NotNull(password.Hash);
        Assert.NotEmpty(password.Hash);
    }

    [Theory]
    [InlineData("12345678")]        // Senha sem letra
    [InlineData("Senha1234")]       // Senha sem especial
    [InlineData("SenhaSemNumero")]  // Senha sem número
    [InlineData("Senha@1")]         // Senha muito curta
    public void Constructor_InvalidPassword_ThrowsArgumentException(string invalidPassword)
    {
        Assert.Throws<ArgumentException>(() => new Password(invalidPassword));
    }
}

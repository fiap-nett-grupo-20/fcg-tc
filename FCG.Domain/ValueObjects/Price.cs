

using FCG.Domain.Exceptions;

namespace FCG.Domain.ValueObjects
{
    public record Price
    {
        public decimal Amount { get; }

        public Price(decimal? amount)
        {
            if (!amount.HasValue)
                throw new BusinessErrorDetailsException("Preço é obrigatório.");
            if (amount.Value < 0)
                throw new BusinessErrorDetailsException("Preço não pode ser negativo.");

            Amount = amount.Value;
        }

        // Implicit conversion to simplify uso no EF e no domínio
        public static implicit operator decimal(Price p) => p.Amount;
        public static explicit operator Price(decimal d) => new Price(d);

        public override string ToString() => Amount.ToString("F2");
    }
}

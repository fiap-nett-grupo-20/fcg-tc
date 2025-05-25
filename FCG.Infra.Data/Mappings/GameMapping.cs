using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;

namespace FCG.Infra.Data.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("games");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(j => j.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(j => j.Description)
                   .IsRequired();

            builder.Property(j => j.Genre)
                   .HasMaxLength(50);

            builder.Property(j => j.Price).HasPrecision(6);

            //builder.Property(j => j.DataLancamento);

            //builder.Property(j => j.Plataforma)
            //       .HasMaxLength(50);

            //builder.Property(j => j.UsuarioId)
            //       .IsRequired();
        }
    }
}

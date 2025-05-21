using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;

namespace FCG.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                .HasConversion(
                    e => e.ToString(),
                    e => new Email(e)
                ).IsRequired();

            builder.Property(u => u.Password)
                .HasConversion(
                    p => p.ToString(),
                    p => new Password(p)
                ).IsRequired();

            builder.Property(u => u.Role)
                  .IsRequired()
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Relacionamentos (exemplo com jogos)
            //builder.HasMany(u => u.Jogos)
            //       .WithOne(j => j.Dono)
            //       .HasForeignKey(j => j.UsuarioId);
        }
    }
}

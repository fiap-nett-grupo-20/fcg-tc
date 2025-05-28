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
            builder.ToTable("AspNetUsers"); // 🔥 Mantém o padrão Identity

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            
        }
    }
}

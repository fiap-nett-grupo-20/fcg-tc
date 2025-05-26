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

            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(email => email.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(100);
            });
            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(pwd => pwd.Hash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasMaxLength(500);
            });

            builder.Property(u => u.Role)
                  .IsRequired()
                  .HasConversion<string>()
                  .HasMaxLength(20);

            
        }
    }
}

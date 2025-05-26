using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;

namespace FCG.Infra.Data.Context
{
    public class FCGDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public FCGDbContext(DbContextOptions<FCGDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.OwnsOne(u => u.Email, e =>
                {
                    e.Property(email => email.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(100);
                });
                entity.OwnsOne(u => u.Password, p =>
                {
                    p.Property(pwd => pwd.Hash)
                    .IsRequired()
                    .HasColumnName("PassworHash")
                    .HasMaxLength(500);
                });
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Games");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(g => g.Price)
                    .IsRequired()
                    .HasConversion(
                        price => (decimal)price,    
                        amount => (Price)amount     
                    )
                    .HasColumnType("decimal(18,2)");
                entity.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(g => g.Genre)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

    }
}

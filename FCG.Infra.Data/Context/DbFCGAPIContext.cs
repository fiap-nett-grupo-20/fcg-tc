using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FCG.Infra.Data.Context
{
    public class DbFCGAPIContext : IdentityDbContext<User>
    {
        public DbFCGAPIContext(DbContextOptions<DbFCGAPIContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de Game
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Games");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(g => g.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(g => g.Genre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // 🔥 Configuração adicional para User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}

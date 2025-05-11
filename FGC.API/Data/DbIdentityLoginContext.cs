using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FGC.API.Data
{

    public class DbIdentityLoginContext : IdentityDbContext<User>
    {
        public DbIdentityLoginContext(DbContextOptions<DbIdentityLoginContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.NameUser)
                      .HasMaxLength(200)
                      .IsRequired();
            });
        }
    }
}
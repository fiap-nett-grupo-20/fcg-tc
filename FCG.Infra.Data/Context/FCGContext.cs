using FCG.Domain.Entities;
using FCG.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Context
{
    public class FCGContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        public FCGContext(DbContextOptions<FCGContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new GameMapping());
        }

    }
}

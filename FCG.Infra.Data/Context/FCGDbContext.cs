using FCG.Domain.Entities;
using FCG.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Context
{
    public class FCGDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        public FCGDbContext(DbContextOptions<FCGDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new GameMapping());
        }

    }
}

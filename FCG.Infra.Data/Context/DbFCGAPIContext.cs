using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Context
{
    public class DbFCGAPIContext : DbContext
    {
        public DbFCGAPIContext(DbContextOptions<DbFCGAPIContext> options) : base(options)
        {
        }

    }
}

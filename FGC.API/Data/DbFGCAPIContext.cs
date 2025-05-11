using Microsoft.EntityFrameworkCore;

namespace FGC.API.Data
{
    public class DbFGCAPIContext : DbContext
    {
        public DbFGCAPIContext(DbContextOptions<DbFGCAPIContext> options) : base(options)
        {
        }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FGC.API.Models;

namespace FGC.API.Data
{
    public class FGCAPIContext : DbContext
    {
        public FGCAPIContext (DbContextOptions<FGCAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FGC.API.Models.Users> Users { get; set; } = default!;
    }
}

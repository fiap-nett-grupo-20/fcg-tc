using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FGC.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FGC.API.Data
{
    public class DbIdentityLoginContext : IdentityDbContext
    {
        public DbIdentityLoginContext (DbContextOptions<DbIdentityLoginContext> options)
            : base(options)
        {
        }


        
    }
}

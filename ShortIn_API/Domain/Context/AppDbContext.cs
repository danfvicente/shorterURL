using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortIn_API.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {

        }
        
    }
}

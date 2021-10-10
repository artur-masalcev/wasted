using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealAPI.Models
{
    public class DealContext : DbContext
    {
        public DealContext(DbContextOptions<DealContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Deal> deals { get; set; }
    }
}

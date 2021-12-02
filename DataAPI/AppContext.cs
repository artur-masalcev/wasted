using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Models
{
    public class AppContext : DbContext
    {
        
        public DbSet<Deal> Deals { get; set; }
        public DbSet<FoodPlace> FoodPlaces { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<PlaceUser> PlaceUsers { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
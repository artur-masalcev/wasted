using DataAPI.Models;
using DataAPI.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAPI
{
    public class AppDbContext : DbContext
    {

        public DbSet<Deal> Deals { get; set; }
        public DbSet<FoodPlace> FoodPlaces { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<PlaceUser> PlaceUsers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<FoodPlaceType> FoodPlaceTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}
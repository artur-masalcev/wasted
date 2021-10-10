using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlaceAPI.Models
{
    public class FoodPlaceContext : DbContext
    {
        public FoodPlaceContext(DbContextOptions<FoodPlaceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<FoodPlace> foodPlaces { get; set; }
    }
}

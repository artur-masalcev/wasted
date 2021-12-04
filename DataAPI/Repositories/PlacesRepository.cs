using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class PlacesRepository
    {
        private readonly AppContext _context;
        public PlacesRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodPlace> Get()
        {
            return _context.FoodPlaces
                .Include(p => p.Deals)
                .Include(p => p.Ratings)
                .ToList();
        }

        public FoodPlace Create(FoodPlace foodPlace)
        {
            _context.FoodPlaces.Add(foodPlace);
            _context.SaveChangesAsync();
            return foodPlace;
        }
    }
}
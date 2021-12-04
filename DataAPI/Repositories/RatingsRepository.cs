using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;

namespace DataAPI.Repositories
{
    public class RatingsRepository
    {
        private readonly AppContext _context;

        public RatingsRepository(AppContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Rating> Get()
        {
            return _context.Ratings
                //.Include(p => p.Deals)
                .ToList();
        }

        public Rating Create(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChangesAsync();
            return rating;
        }
    }
}
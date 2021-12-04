using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;

namespace DataAPI.Repositories
{
    public class RatingsRepository
    {
        private readonly AppDbContext _dbContext;

        public RatingsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Rating> Get()
        {
            return _dbContext.Ratings
                //.Include(p => p.Deals)
                .ToList();
        }

        public Rating Create(Rating rating)
        {
            _dbContext.Ratings.Add(rating);
            _dbContext.SaveChangesAsync();
            return rating;
        }
    }
}
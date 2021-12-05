using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class PlaceUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public PlaceUsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<PlaceUser> Get()
        {
            return _dbContext.PlaceUsers
                .Include(p => p.FoodPlaces)
                .ThenInclude(f => f.Deals)
                .ToList();
        }

        public PlaceUser Create(PlaceUser placeUser)
        {
            _dbContext.PlaceUsers.Add(placeUser);
            _dbContext.SaveChangesAsync();
            return placeUser;
        }
    }
}
using System.Collections.Generic;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class FoodPlaceTypesRepository
    {
        private readonly AppDbContext _dbContext;

        public FoodPlaceTypesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FoodPlaceType> Get()
        {
            return _dbContext.FoodPlaceTypes
                .Include(type => type.FoodPlaces)
                .ThenInclude(place => place.Ratings)
                .Include(type => type.FoodPlaces)
                .ThenInclude(place => place.Deals);
        }

        public FoodPlaceType Create(FoodPlaceType foodPlaceType)
        {
            _dbContext.FoodPlaceTypes.Add(foodPlaceType);
            _dbContext.SaveChangesAsync();
            return foodPlaceType;
        }
    }
}
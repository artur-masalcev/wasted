using System.Collections.Generic;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class FoodPlaceTypeRepository
    {
        private readonly AppDbContext _dbContext;
        public FoodPlaceTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FoodPlaceType> Get()
        {
            return _dbContext.FoodPlaceTypes
                .Include(type => type.FoodPlaces);
        }

        public FoodPlaceType Create(FoodPlaceType foodPlaceType)
        {
            _dbContext.FoodPlaceTypes.Add(foodPlaceType);
            _dbContext.SaveChangesAsync();
            return foodPlaceType;
        }
    }
}
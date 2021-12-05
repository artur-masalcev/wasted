using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class PlacesRepository
    {
        private readonly AppDbContext _dbContext;
        public PlacesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FoodPlace> Get()
        {
            return _dbContext.FoodPlaces
                .Include(p => p.Deals)
                .Include(p => p.Ratings)
                .Include(p => p.PlaceType)
                //.Include(p => p.PlaceUser)
                .ToList();
        }

        public FoodPlace Create(FoodPlace foodPlace)
        {
            _dbContext.FoodPlaces.Add(foodPlace);
            _dbContext.SaveChangesAsync();
            return foodPlace;
        }

        public void Delete(int foodPlaceId)
        {
            FoodPlace placeToDelete = _dbContext.FoodPlaces.Find(foodPlaceId);
            _dbContext.FoodPlaces.Remove(placeToDelete);
            _dbContext.SaveChangesAsync();
        }
        
    }
}
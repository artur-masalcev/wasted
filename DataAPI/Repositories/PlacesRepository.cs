using System.Collections.Generic;
using System.Linq;
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

        public void Update(FoodPlace updatedFoodPlace)
        {
            var selectedFoodPlace = _dbContext.FoodPlaces.Find(updatedFoodPlace.Id);

            selectedFoodPlace.City = updatedFoodPlace.City;
            selectedFoodPlace.Description = updatedFoodPlace.Description;
            selectedFoodPlace.Latitude = updatedFoodPlace.Latitude;
            selectedFoodPlace.Longitude = updatedFoodPlace.Longitude;
            selectedFoodPlace.Street = updatedFoodPlace.Street;
            selectedFoodPlace.Title = updatedFoodPlace.Title;
            selectedFoodPlace.WorkingHours = updatedFoodPlace.WorkingHours;
            selectedFoodPlace.HeaderURL = updatedFoodPlace.HeaderURL;
            selectedFoodPlace.LogoURL = updatedFoodPlace.LogoURL;

            _dbContext.SaveChangesAsync();
        }
    }
}
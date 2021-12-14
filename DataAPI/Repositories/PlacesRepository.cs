using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{
    public class PlacesRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public PlacesRepository(AppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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
            _logger.Information(
                "A new food place \"{FoodPlaceTitle}\" (food place id: {FoodPlace}) has been added.\n",
                foodPlace.Title, foodPlace.Id);
            _dbContext.SaveChangesAsync();
            return foodPlace;
        }

        public void Delete(int foodPlaceId)
        {
            var placeToDelete = _dbContext.FoodPlaces.Find(foodPlaceId);
            _dbContext.FoodPlaces.Remove(placeToDelete);
            _logger.Information(
                "Food place \"{FoodPlaceTitle}\" (food place id: {FoodPlace}) has been deleted.\n",
                placeToDelete.Title, placeToDelete.Id);
            _dbContext.SaveChangesAsync();
        }

        public void Update(FoodPlace updatedFoodPlace)
        {
            var selectedFoodPlace = _dbContext.FoodPlaces.Find(updatedFoodPlace.Id);

            selectedFoodPlace.City = updatedFoodPlace.City;
            selectedFoodPlace.Description = updatedFoodPlace.Description;
            selectedFoodPlace.Street = updatedFoodPlace.Street;
            selectedFoodPlace.Title = updatedFoodPlace.Title;
            selectedFoodPlace.WorkingHours = updatedFoodPlace.WorkingHours;
            selectedFoodPlace.HeaderURL = updatedFoodPlace.HeaderURL;
            selectedFoodPlace.LogoURL = updatedFoodPlace.LogoURL;

            _dbContext.SaveChangesAsync();
        }
    }
}
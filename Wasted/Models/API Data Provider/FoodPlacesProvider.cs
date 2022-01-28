using System.Collections.Generic;
using DataAPI.DTO;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Essentials;

namespace Wasted.WastedAPI
{
    public static class FoodPlacesProvider
    {
        private const string FoodPlacesRoute = "foodplaces";
        private const string PlaceTypeRoute = "foodplacetypes";
        private const string RatingsRoute = "ratings";

        public static FoodPlace GetFoodPlaceById(int id)
        {
            return DataFetchingUtils.GetBusinessObject<FoodPlace>(FoodPlacesRoute, id);
        }

        public static List<FoodPlace> GetClosestFoodPlaces(Location userLocation, int nearbyPlacesCount,
            int maxRadiusInKilometers)
        {
            return DataFetchingUtils.GetBusinessObject<List<FoodPlace>>(FoodPlacesRoute, userLocation.Longitude,
                userLocation.Latitude, nearbyPlacesCount, maxRadiusInKilometers);
        }

        public static List<FoodPlace> GetTopRatedFoodPlaces(int popularPlacesCount)
        {
            return DataFetchingUtils.GetBusinessObject<List<FoodPlace>>(FoodPlacesRoute, popularPlacesCount);
        }

        public static void CreateFoodPlace(FoodPlace foodPlace)
        {
            DataFetchingUtils.CreateBusinessObject(foodPlace, FoodPlacesRoute);
        }

        public static void DeleteFoodPlace(FoodPlace foodPlace)
        {
            DataFetchingUtils.DeleteBusinessObject( FoodPlacesRoute, foodPlace.Id);
        }

        public static void UpdateFoodPlace(FoodPlace foodPlace)
        {
            DataFetchingUtils.UpdateBusinessObject(foodPlace, FoodPlacesRoute);
        }

        public static void CreateRating(RatingDTO rating)
        {
            DataFetchingUtils.CreateBusinessObject(rating, RatingsRoute);
        }

        public static void UpdateRating(RatingDTO rating)
        {
            DataFetchingUtils.UpdateBusinessObject(rating, RatingsRoute);
        }

        public static List<FoodPlaceType> GetFoodPlaceTypes()
        {
            return DataFetchingUtils.GetBusinessObject<List<FoodPlaceType>>(PlaceTypeRoute);
        }
    }
}
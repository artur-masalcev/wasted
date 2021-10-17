using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Wasted.DummyAPI.BusinessObjects;

namespace DataAPI.Utils
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {

        private const string FoodPlacesJSONPath = "Dummy Data/FoodPlaces.json";

        private const string DealsJSONPath = "Dummy Data/Deals.json";

        private const string RatingsJSONPath = "Dummy Data/Ratings.json";

        public static List<FoodPlace> GetFoodPlaces()
        {
            return JsonConvert.DeserializeObject<List<FoodPlace>>(GetText(FoodPlacesJSONPath));
        }

        public static List<Deal> GetDeals()
        {
            return JsonConvert.DeserializeObject<List<Deal>>(GetText(DealsJSONPath));
        }

        public static string GetRatingsDictionary()
        {
            return GetText(RatingsJSONPath);
        }


        public static string GetText(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

    }
}

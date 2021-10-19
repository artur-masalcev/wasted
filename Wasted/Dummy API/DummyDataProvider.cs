﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;

namespace Wasted.DummyDataAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {
        /// <summary>
        /// Path to food providers embeedded resource JSON file
        /// </summary>
        private const string FoodPlacesJSONPath = "Wasted.Dummy_API.Dummy_Data.FoodPlaces.json";

        /// <summary>
        /// Path to deals embeedded resource JSON file
        /// </summary>
        private const string DealsJSONPath = "Wasted.Dummy_API.Dummy_Data.Deals.json";

        /// <summary>
        /// Path to foodPlace ratings embeedded resource JSON file
        /// </summary>
        private const string RatingsJSONPath = "Wasted.Dummy_API.Dummy_Data.Ratings.json";

        /// <summary>
        /// Place types that are shown in the search page
        /// </summary>
        public static string[] placeTypes = new string[]{ "Restaurant", "Supermarket", "Person" };

        public static Dictionary<string, int> placeTypeDictionary = GetIndexDictionary(placeTypes);

        /// <summary>
        /// Creates a hashmap from string to corresponding index
        /// </summary>
        public static Dictionary<string, int> GetIndexDictionary(string[] strings)
        {
            Dictionary<string, int> indexDictionary = new Dictionary<string, int>();
            for (int i = 0; i < strings.Length; ++i)
            {
                indexDictionary[strings[i]] = i;
            }
            return indexDictionary;
        }

        /// <summary>
        /// Provides a list of all food providers in JSON format string
        /// </summary>
        /// <returns>string containing all food providers in JSON format</returns>
        public static string GetFoodPlaces()
        {
            return EmbeddedDataReader.ReadString(FoodPlacesJSONPath);
        }

        /// <summary>
        /// Provides a list of all deals in JSON format string
        /// </summary>
        /// <returns>string containing all deals in JSON format</returns>
        public static string GetDeals()
        {
            return EmbeddedDataReader.ReadString(DealsJSONPath);
        }

        /// <summary>
        /// Provides a list of all deals in JSON format string
        /// </summary>
        /// <returns>string containing all deals in JSON format</returns>
        public static string GetRatings()
        {
            return EmbeddedDataReader.ReadString(RatingsJSONPath);
        }

        /// <summary>
        /// Provides a list of all food providers in List<FoodPlace> format
        /// </summary>
        /// <returns>list containing all food providers from JSON data</returns>
        public static IEnumerable<FoodPlace> GetFoodPlacesList()
        {
            return JsonConvert.DeserializeObject<List<FoodPlace>>(GetFoodPlaces());
        }

        /// <summary>
        /// Provides a list of all deals in List<FoodPlace> format
        /// </summary>
        /// <returns>list containing all deals from JSON data</returns>
        public static IEnumerable<Deal> GetDealsList()
        {
            return JsonConvert.DeserializeObject<List<Deal>>(GetDeals());
        }

        /// <summary>
        /// Provides a dictionary in <UserID, <FoodPlaceID, Rating>> format
        /// </summary>
        /// <returns>dictionary containing all ratings from user</returns>
        public static Dictionary<int, Dictionary<int, int>> GetRatingsDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(GetRatings());
        }

        /// <summary>
        /// Writes all food places to device
        /// </summary>
        public static void WriteFoodPlacesList()
        {
            EmbeddedDataReader.WriteString(FoodPlacesJSONPath, JsonConvert.SerializeObject(App.AllFoodPlaces));
        }

        /// <summary>
        /// Writes all deals to device
        /// </summary>
        public static void WriteDealsList()
        {
            EmbeddedDataReader.WriteString(DealsJSONPath, JsonConvert.SerializeObject(App.AllDeals));
        }

        /// <summary>
        /// Writes all ratings to device
        /// </summary>
        public static void WriteRatingsDictionary()
        {
            EmbeddedDataReader.WriteString(RatingsJSONPath, JsonConvert.SerializeObject(App.Ratings));
        }
    }
}

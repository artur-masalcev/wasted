using Newtonsoft.Json;
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
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using System.Configuration;
using System.Reflection;
using Xamarin.Forms;

namespace Wasted.DummyDataAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {
        public static string IP = ConfigurationProperties.LocalIPAddress;
        public static string linkStart = "http://" + IP + ":5001/";

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


        public static async Task<List<FoodPlace>> GetFoodPlaces(HttpClient client)
        {
            string foodPlaceJson = await client.GetStringAsync(linkStart + "foodplaces");
            return JsonConvert.DeserializeObject<List<FoodPlace>>(foodPlaceJson);
        }

        public static async Task<List<Deal>> GetDeals(HttpClient client)
        {
            string dealJson = await client.GetStringAsync(linkStart + "deals");
            return JsonConvert.DeserializeObject<List<Deal>>(dealJson);
        }

        public static async Task<Dictionary<int, Dictionary<int, int>>> GetRatings(HttpClient client)
        {
            string ratingJson = await client.GetStringAsync(linkStart + "ratings");
            return JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, int>>>(ratingJson);
        }
    }
}

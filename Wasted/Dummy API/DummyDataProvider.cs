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
using System.Text;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted.DummyDataAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {
        public static string IP = ConfigurationProperties.LocalIPAddress;
        public static string linkStart = "http://" + IP + ":5001/";


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

        public static async Task<Dictionary<string, UserClient>> GetClientUsers(HttpClient client)
        {
            string userJson = await client.GetStringAsync(linkStart + "clientusers");
            return JsonConvert.DeserializeObject<Dictionary<string, UserClient>>(userJson);
        }
        
        public static async Task<Dictionary<string, UserPlace>> GetPlaceUsers(HttpClient client)
        {
            string userJson = await client.GetStringAsync(linkStart + "placeusers");
            return JsonConvert.DeserializeObject<Dictionary<string, UserPlace>>(userJson);
        }


        public static async Task WriteFoodPlaces(HttpClient client, List<FoodPlace> AllFoodPlaces)
        {
            await client.PostAsync(linkStart + "foodplaces/add", GetStringContent(AllFoodPlaces));
        }

        public static async Task WriteDeals(HttpClient client, List<Deal> AllDeals)
        {
            await client.PostAsync(linkStart + "deals/add", GetStringContent(AllDeals));
        }

        public static async Task WritePlaceUsers(HttpClient client, Dictionary<string, UserPlace> Users)
        {
            await client.PostAsync(linkStart + "placeusers/add", GetStringContent(Users));
        }
        
        public static async Task WriteClientUsers(HttpClient client, Dictionary<string, UserClient> Users)
        {
            await client.PostAsync(linkStart + "clientusers/add", GetStringContent(Users));
        }

        public static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(JsonConvert.SerializeObject(obj));
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}

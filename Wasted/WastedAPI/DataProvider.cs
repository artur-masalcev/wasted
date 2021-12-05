using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using DataAPI.DTO;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Properties;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.DummyDataAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public static class DataProvider
    {
        public static string IP => ConfigurationProperties.LocalIPAddress;
        public static string LinkStart => "http://" + IP + ":5001/";

        public static string FoodPlacesEnd => "foodplaces";
        public static string DealsEnd => "deals";
        public static string ClientUsersEnd => "clientusers";
        public static string PlaceUsersEnd => "placeusers";
        public static string PlaceTypeEnd => "foodplacetypes";

        public static string RatingsEnd => "ratings";

        public static string ClientUserEnd(string name, string password)
        {
            return String.Join("/", ClientUsersEnd, name, password);
        }
        
        public static string PlaceUserEnd(string name, string password)
        {
            return String.Join("/", PlaceUsersEnd, name, password);
        }

        private static HttpClient Client = new HttpClient();
        private delegate T DefaultObject<T>();

        /// <summary>
        /// Gets data from API
        /// </summary>
        public static async Task<T> GetData<T>(string linkEnd)
        {
            string dataJson = await Client.GetStringAsync(LinkStart + linkEnd);
            return JsonConvert.DeserializeObject<T>(dataJson); 
        }
        
        private static void CreateBusinessObject<T>(T data, string linkEnd)
        {
            Task.Run(async () =>
                await Client.PostAsync(LinkStart + linkEnd, GetStringContent(data))).Wait();
        }

        private static void UpdateBusinessObject<T>(T data, string linkEnd)
        {
            Task.Run(async () =>
                await Client.PutAsync(LinkStart + linkEnd, GetStringContent(data))).Wait();
        }
        
        private static void DeleteBusinessObject(int id, string linkEnd)
        {
            Task.Run(async () =>
                await Client.DeleteAsync(LinkStart + linkEnd + "/" + id)).Wait();
        }

        /// <summary>
        /// Converts object to json StringContent. Serializing content once is not enough
        /// </summary>
        private static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public static void CreateDeal(Deal deal) => CreateBusinessObject(deal, DealsEnd);
        public static void UpdateDeal(Deal deal) => UpdateBusinessObject(deal, DealsEnd);
        
        public static void CreateFoodPlace(FoodPlace foodPlace) => CreateBusinessObject(foodPlace, FoodPlacesEnd);
        public static void DeleteFoodPlace(FoodPlace foodPlace) => DeleteBusinessObject(foodPlace.Id, FoodPlacesEnd);

        public static void CreateClientUser(ClientUser clientUser) => CreateBusinessObject(clientUser, ClientUsersEnd);
        public static void CreatePlaceUser(UserPlace placeUser) => CreateBusinessObject(placeUser, PlaceUsersEnd);

        public static void CreateRating(RatingDTO rating) => CreateBusinessObject(rating, RatingsEnd);
        public static void UpdateRating(RatingDTO rating) => UpdateBusinessObject(rating, RatingsEnd);
    }
}
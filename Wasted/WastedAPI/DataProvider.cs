using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAPI.DTO;
using Newtonsoft.Json;
using Wasted.Properties;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;

namespace Wasted.WastedAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public static class DataProvider
    {
        private static string Ip => ConfigurationProperties.LocalIpAddress;
        private static string LinkStart => "http://" + Ip + ":5001/";

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

        private static readonly HttpClient Client = new HttpClient();

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

        public static void DeleteDeal(Deal deal) => DeleteBusinessObject(deal.Id, DealsEnd);
        
        public static void CreateFoodPlace(FoodPlace foodPlace) => CreateBusinessObject(foodPlace, FoodPlacesEnd);
        public static void DeleteFoodPlace(FoodPlace foodPlace) => DeleteBusinessObject(foodPlace.Id, FoodPlacesEnd);

        public static void UpdateFoodPlace(FoodPlace foodPlace) => UpdateBusinessObject(foodPlace, FoodPlacesEnd);

        public static void CreateClientUser(ClientUser clientUser) => CreateBusinessObject(clientUser, ClientUsersEnd);
        public static void CreatePlaceUser(PlaceUser placeUser) => CreateBusinessObject(placeUser, PlaceUsersEnd);

        public static void CreateRating(RatingDTO rating) => CreateBusinessObject(rating, RatingsEnd);
        public static void UpdateRating(RatingDTO rating) => UpdateBusinessObject(rating, RatingsEnd);
    }
}
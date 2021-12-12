using System;
using System.Collections.Generic;
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
        private static readonly HttpClient Client = new HttpClient();

        private static string FoodPlacesEnd => "foodplaces";
        private static string DealsEnd => "deals";
        private static string ClientUsersEnd => "clientusers";
        private static string PlaceUsersEnd => "placeusers";
        private static string PlaceTypeEnd => "foodplacetypes";
        private static string RatingsEnd => "ratings";
        private static string ClientUserEnd(string name, string password) => string.Join("/", ClientUsersEnd, name, password);
        private static string PlaceUserEnd(string name, string password) => string.Join("/", PlaceUsersEnd, name, password);
   
        

        /// <summary>
        /// Gets data from API
        /// </summary>
        private static T GetData<T>(string linkEnd)
        {
            string dataJson = Task.Run(async () => await Client.GetStringAsync(LinkStart + linkEnd)).Result;
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
        /// Converts object to json StringContent.
        /// </summary>
        private static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public static List<Deal> GetAllDeals() => GetData<List<Deal>>(DealsEnd);
        public static void CreateDeal(Deal deal) => CreateBusinessObject(deal, DealsEnd);
        public static void UpdateDeal(Deal deal) => UpdateBusinessObject(deal, DealsEnd);

        public static List<FoodPlace> GetAllFoodPlaces() => GetData<List<FoodPlace>>(FoodPlacesEnd);
        public static void CreateFoodPlace(FoodPlace foodPlace) => CreateBusinessObject(foodPlace, FoodPlacesEnd);
        public static void DeleteFoodPlace(FoodPlace foodPlace) => DeleteBusinessObject(foodPlace.Id, FoodPlacesEnd);

        public static ClientUser GetClientUser(string username, string password) => GetData<ClientUser>(ClientUserEnd(username, password));

        public static void CreateClientUser(ClientUser clientUser) => CreateBusinessObject(clientUser, ClientUsersEnd);

        public static PlaceUser GetPlaceUser(string username, string password) => GetData<PlaceUser>(PlaceUserEnd(username, password));
        public static void CreatePlaceUser(PlaceUser placeUser) => CreateBusinessObject(placeUser, PlaceUsersEnd);

        public static void CreateRating(RatingDTO rating) => CreateBusinessObject(rating, RatingsEnd);
        public static void UpdateRating(RatingDTO rating) => UpdateBusinessObject(rating, RatingsEnd);

        public static List<FoodPlaceType> GetFoodPlaceTypes() => GetData<List<FoodPlaceType>>(PlaceTypeEnd);
    }
}
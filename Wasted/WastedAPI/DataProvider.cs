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
        private static string OrdersEnd => "orders";
        private static string JoinParams(params string[] linkParams) => string.Join("/", linkParams);


        /// <summary>
        /// Gets data from API
        /// </summary>
        private static T GetBusinessObject<T>(string linkEnd)
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

        public static List<Deal> GetAllDeals() => GetBusinessObject<List<Deal>>(DealsEnd);
        public static void CreateDeal(Deal deal) => CreateBusinessObject(deal, DealsEnd);
        public static void UpdateDeal(Deal deal) => UpdateBusinessObject(deal, DealsEnd);

        public static List<FoodPlace> GetAllFoodPlaces() => GetBusinessObject<List<FoodPlace>>(FoodPlacesEnd);
        public static void CreateFoodPlace(FoodPlace foodPlace) => CreateBusinessObject(foodPlace, FoodPlacesEnd);
        public static void DeleteFoodPlace(FoodPlace foodPlace) => DeleteBusinessObject(foodPlace.Id, FoodPlacesEnd);

        public static ClientUser GetClientUser(string username, string password) =>
            GetBusinessObject<ClientUser>(JoinParams(ClientUsersEnd, username, password));

        public static void CreateClientUser(ClientUser clientUser) => CreateBusinessObject(clientUser, ClientUsersEnd);

        public static PlaceUser GetPlaceUser(string username, string password) =>
            GetBusinessObject<PlaceUser>(JoinParams(PlaceUsersEnd, username, password));
        public static void CreatePlaceUser(PlaceUser placeUser) => CreateBusinessObject(placeUser, PlaceUsersEnd);

        public static void CreateRating(RatingDTO rating) => CreateBusinessObject(rating, RatingsEnd);
        public static void UpdateRating(RatingDTO rating) => UpdateBusinessObject(rating, RatingsEnd);

        public static List<FoodPlaceType> GetFoodPlaceTypes() => GetBusinessObject<List<FoodPlaceType>>(PlaceTypeEnd);

        public static List<OrderDeal> GetClientOrders(int clientUserId) =>
            GetBusinessObject<List<OrderDeal>>(JoinParams(OrdersEnd, ClientUsersEnd, clientUserId.ToString()));
        public static List<OrderDeal> GetPlaceOrders(int placeUserId) =>
            GetBusinessObject<List<OrderDeal>>(JoinParams(OrdersEnd, PlaceUsersEnd, placeUserId.ToString()));
        public static void CreateOrder(OrderDeal orderDeal) => CreateBusinessObject(orderDeal, OrdersEnd);
        public static void UpdateOrders(List<OrderDeal> orderDeals) =>
            UpdateBusinessObject(orderDeals, OrdersEnd);
    }
}
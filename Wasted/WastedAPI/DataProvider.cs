﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAPI.DTO;
using DataAPI.Models.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wasted.Properties;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Essentials;

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
        private static string UsersEnd => "users";
        private static string ClientUsersEnd => "clientusers";
        private static string PlaceUsersEnd => "placeusers";
        private static string PlaceTypeEnd => "foodplacetypes";
        private static string RatingsEnd => "ratings";
        private static string OrdersEnd => "orders";
        
        private static readonly Dictionary<string, Func<string, User>> UserGettingFunctions = new Dictionary<string, Func<string, User>>
        {
            {Constants.ClientType, JsonConvert.DeserializeObject<ClientUser>},
            {Constants.PlaceType, JsonConvert.DeserializeObject<PlaceUser>}
        };


        public static List<Deal> GetAllDeals() => GetBusinessObject<List<Deal>>(DealsEnd);
        public static List<Deal> GetBestOffers(int specialOffersCount) => GetBusinessObject<List<Deal>>(JoinParams(DealsEnd, specialOffersCount));
        public static void CreateDeal(Deal deal) => CreateBusinessObject(deal, DealsEnd);
        public static void UpdateDeal(Deal deal) => UpdateBusinessObject(deal, DealsEnd);
        public static void DeleteDeal(Deal deal) => DeleteBusinessObject(deal.Id, DealsEnd);

        public static List<FoodPlace> GetAllFoodPlaces() => GetBusinessObject<List<FoodPlace>>(FoodPlacesEnd);

        public static List<FoodPlace> GetClosestFoodPlaces(Location userLocation, int nearbyPlacesCount, int maxRadiusInKilometers) =>
            GetBusinessObject<List<FoodPlace>>(JoinParams(FoodPlacesEnd, userLocation.Longitude,
                userLocation.Latitude, nearbyPlacesCount, maxRadiusInKilometers));

        public static List<FoodPlace> GetTopRatedFoodPlaces(int popularPlacesCount) =>
            GetBusinessObject<List<FoodPlace>>(JoinParams(FoodPlacesEnd, popularPlacesCount));
        
        public static void CreateFoodPlace(FoodPlace foodPlace) => CreateBusinessObject(foodPlace, FoodPlacesEnd);
        public static void DeleteFoodPlace(FoodPlace foodPlace) => DeleteBusinessObject(foodPlace.Id, FoodPlacesEnd);
        public static void UpdateFoodPlace(FoodPlace foodPlace) => UpdateBusinessObject(foodPlace, FoodPlacesEnd);

        public static ClientUser GetClientUser(UserDetailsService service) =>
            GetBusinessObject<ClientUser>(JoinParams(ClientUsersEnd, service.UserName, service.UserName));

        public static void CreateClientUser(ClientUser clientUser) => CreateBusinessObject(clientUser, ClientUsersEnd);

        public static PlaceUser GetPlaceUser(UserDetailsService service) =>
            GetBusinessObject<PlaceUser>(JoinParams(PlaceUsersEnd, service.UserName, service.UserPassword));

        public static void CreatePlaceUser(PlaceUser placeUser) => CreateBusinessObject(placeUser, PlaceUsersEnd);

        public static User GetUser(string username, string password)
        {
            string endpoint = JoinParams(UsersEnd, username, password);
            string json = GetJson(endpoint);
            string userType = JObject.Parse(json)[Constants.UserType]?.ToString();
            if (userType == null)
                throw new JsonException($"Error. Expected {Constants.UserType} attribute in user json.");

            return UserGettingFunctions[userType].Invoke(json);
        }
        public static void CreateRating(RatingDTO rating) => CreateBusinessObject(rating, RatingsEnd);
        public static void UpdateRating(RatingDTO rating) => UpdateBusinessObject(rating, RatingsEnd);

        public static List<FoodPlaceType> GetFoodPlaceTypes() => GetBusinessObject<List<FoodPlaceType>>(PlaceTypeEnd);

        public static List<OrderDeal> GetClientOrders(int clientUserId) =>
            GetBusinessObject<List<OrderDeal>>(JoinParams(OrdersEnd, ClientUsersEnd, clientUserId));

        public static List<OrderDeal> GetPlaceOrders(int placeUserId) =>
            GetBusinessObject<List<OrderDeal>>(JoinParams(OrdersEnd, PlaceUsersEnd, placeUserId));

        public static void CreateOrder(OrderDeal orderDeal) => CreateBusinessObject(orderDeal, OrdersEnd);

        public static void UpdateOrders(List<OrderDeal> orderDeals) =>
            UpdateBusinessObject(orderDeals, OrdersEnd);
        
        private static string JoinParams(params object[] linkParams) => string.Join("/", linkParams);

        // <summary>
        /// Gets data from API
        /// </summary>
        private static T GetBusinessObject<T>(string linkEnd)
        {
            string dataJson = GetJson(linkEnd);
            return JsonConvert.DeserializeObject<T>(dataJson);
        }

        private static string GetJson(string linkEnd)
        {
            return Client.GetStringAsync(LinkStart + linkEnd).Result;
        }

        private static void CreateBusinessObject<T>(T data, string linkEnd)
        {
            Client.PostAsync(LinkStart + linkEnd, GetStringContent(data)).Wait();
        }

        private static void UpdateBusinessObject<T>(T data, string linkEnd)
        {
            Client.PutAsync(LinkStart + linkEnd, GetStringContent(data)).Wait();
        }

        private static void DeleteBusinessObject(int id, string linkEnd)
        {
            Client.DeleteAsync(LinkStart + linkEnd + "/" + id).Wait();
        }

        private static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
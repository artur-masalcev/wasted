using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
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

        /// <summary>
        /// Writes data to API
        /// </summary>
        private static void WriteData<T>(T data, DefaultObject<T> defaultObject, string linkEnd)
        {
            Task.Run(async () =>
            {
                T dummyVal = data == null ? defaultObject.Invoke() : data;
                return await Client.PostAsync(LinkStart + linkEnd + "/add", GetStringContent(dummyVal));
            });
        }

        /// <summary>
        /// Converts object to json StringContent. Serializing content once is not enough
        /// </summary>
        public static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(JsonConvert.SerializeObject(obj));
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public static void WriteAllDeals(List<Deal> allDeals = null)
        {
            // WriteData(allDeals, delegate
            // {
            //     DataService service = DependencyService.Get<DataService>();
            //     return service.AllDeals;
            // }, DealsEnd);
        }
        
        public static void WriteAllPlaces(List<FoodPlace> allFoodPlaces = null)
        {
            // WriteData(allFoodPlaces, delegate
            // {
            //     DataService service = DependencyService.Get<DataService>();
            //     return service.AllFoodPlaces;
            // }, FoodPlacesEnd);
        }

        public static void WriteAllUserPlaces(Dictionary<string, UserPlace> placeUsers = null)
        {
            // WriteData(placeUsers, delegate
            // {
            //     DataService service = DependencyService.Get<DataService>();
            //     return service.PlaceUsers;
            // }, PlaceUsersEnd);
        }

        public static void WriteAllUserClients(ClientUser clientUsers = null)
        {
            // WriteData(clientUsers, delegate
            // {
            //     DataService service = DependencyService.Get<DataService>();
            //     return service.ClientUsers;
            // }, PlaceUsersEnd);
        }
    }
}
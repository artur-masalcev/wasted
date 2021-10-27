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
        private DataService service;
        
        public static string IP = ConfigurationProperties.LocalIPAddress;
        public static string linkStart = "http://" + IP + ":5001/";

        public static string FoodPlacesEnd => "foodplaces";
        public static string DealsEnd => "deals";
        public static string ClientUsersEnd => "clientusers";
        public static string PlaceUsersEnd => "placeusers";
        public DummyDataProvider()
        {
            service = DependencyService.Get<DataService>();
            service.Client = new HttpClient();
        }
        public async Task<T> GetData<T>(string linkEnd)
        {
            string dataJson = await service.Client.GetStringAsync(linkStart + linkEnd);
            return JsonConvert.DeserializeObject<T>(dataJson); 
        }

        public async Task WriteData<T>(T data, string linkEnd)
        {
            await service.Client.PostAsync(linkStart + linkEnd + "/add", GetStringContent(data));
        }

        public StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(JsonConvert.SerializeObject(obj));
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}

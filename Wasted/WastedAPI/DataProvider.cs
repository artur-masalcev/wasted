using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Wasted.Properties;

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
        public static async Task WriteData<T>(T data, string linkEnd)
        {
            await Client.PostAsync(LinkStart + linkEnd + "/add", GetStringContent(data));
        }

        /// <summary>
        /// Converts object to json StringContent. Serializing content once is not enough
        /// </summary>
        public static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(JsonConvert.SerializeObject(obj));
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
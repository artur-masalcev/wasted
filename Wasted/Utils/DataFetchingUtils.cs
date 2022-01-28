using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Wasted.Properties;

namespace Wasted.Utils
{
    public static class DataFetchingUtils
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string LinkStart => "http://" + ConfigurationProperties.LocalIpAddress + ":5001/";
        public static T GetBusinessObject<T>(params object[] linkParams)
        {
            return JsonConvert.DeserializeObject<T>(GetJson(linkParams));
        }

        public static void CreateBusinessObject<T>(T data, params object[] linkParams)
        {
            Client.PostAsync(JoinParams(linkParams), GetStringContent(data)).Wait();
        }

        public static void UpdateBusinessObject<T>(T data, params object[] linkParams)
        {
            Client.PutAsync(JoinParams(linkParams), GetStringContent(data)).Wait();
        }

        public static void DeleteBusinessObject(params object[] linkParams)
        {
            Client.DeleteAsync(JoinParams(linkParams)).Wait();
        }

        public static string GetJson(params object[] linkParams)
        {
            return Client.GetStringAsync(JoinParams(linkParams)).Result;
        }

        private static string JoinParams(params object[] linkParams) => LinkStart + string.Join("/", linkParams);

        private static StringContent GetStringContent(object obj)
        {
            string content = JsonConvert.SerializeObject(obj);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
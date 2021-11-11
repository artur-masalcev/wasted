using System.IO;

namespace DataAPI.Utils
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {

        public static string FoodPlacesJSONPath => "Dummy Data/FoodPlaces.json";
        public static string DealsJSONPath => "Dummy Data/Deals.json";
        public static string PlaceUsersJSONPath => "Dummy Data/PlaceUsers.json";

        public static string ClientUsersJSONPath => "Dummy Data/ClientUsers.json";

        public static void WriteText(string path, string content)
        {
            using StreamWriter file = new StreamWriter(path);
            file.WriteLine(content);
        }

        public static string GetText(string path)
        {
            return File.ReadAllText(path);
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static string UsersJSONPath => "Dummy Data/Users.json";

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

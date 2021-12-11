using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DataAPI.Utils
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public static class DummyDataProvider
    {
        private static ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim();
        public static string FoodPlacesJSONPath => "Dummy Data/FoodPlaces.json";
        public static string DealsJSONPath => "Dummy Data/Deals.json";
        public static string PlaceUsersJSONPath => "Dummy Data/PlaceUsers.json";

        public static string ClientUsersJSONPath => "Dummy Data/ClientUsers.json";

        public static void WriteText(string path, string content)
        {
            readWriteLock.EnterWriteLock();
            try
            {
                using StreamWriter file = new StreamWriter(path);
                file.WriteLine(content);
            }
            finally
            {
                readWriteLock.ExitWriteLock();
            }
        }

        public static string GetText(string path)
        {
            return File.ReadAllText(path);
        }

    }
}
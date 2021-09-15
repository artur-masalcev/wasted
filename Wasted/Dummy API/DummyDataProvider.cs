using Wasted.Utils;

namespace Wasted.DummyDataAPI
{
    /// <summary>
    /// Provides methods for receiving local dummy data needed for Wasted app
    /// </summary>
    public class DummyDataProvider
    {
        /// <summary>
        /// Path to food providers embeedded resource JSON file
        /// </summary>
        private const string ProvidersJSONPath = "Wasted.Dummy_API.Dummy_Data.Providers.json";

        /// <summary>
        /// Path to deals embeedded resource JSON file
        /// </summary>
        private const string DealsJSONPath = "Wasted.Dummy_API.Dummy_Data.Deals.json";

        /// <summary>
        /// Provides a list of all food providers in JSON format string
        /// </summary>
        /// <returns>string containing all food providers in JSON format</returns>
        public static string GetProviders()
        {
            return EmbeddedDataReader.ReadString(ProvidersJSONPath);
        }

        /// <summary>
        /// Provides a list of all deals in JSON format string
        /// </summary>
        /// <returns>string containing all deals in JSON format</returns>
        public static string GetDeals()
        {
            return EmbeddedDataReader.ReadString(DealsJSONPath);
        }
    }
}

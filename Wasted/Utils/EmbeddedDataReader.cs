using System.IO;
using System.Reflection;

namespace Wasted.Utils
{
    public class EmbeddedDataReader
    {
        /// <summary>
        /// Reads the data from embedded data in the app
        /// </summary>
        /// <param name="path">Path to the data</param>
        /// <returns>String containing the embedded data</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when there is not data in the given path</exception>
        public static string ReadString(string path)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(path);
            string text = "";

            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }
    }
}

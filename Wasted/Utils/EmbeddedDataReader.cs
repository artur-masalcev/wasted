using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

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

        private static string ReadStringFirstTime(string path)
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

        /// <summary>
        /// Gets content in string format from file
        /// </summary>
        public static string ReadString(string filePath)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filename = Path.Combine(path, filePath);
            if (File.Exists(filename)) //If file is already created from the function above
                return File.ReadAllText(filename);
            return ReadStringFirstTime(filePath);
        }
    }
}

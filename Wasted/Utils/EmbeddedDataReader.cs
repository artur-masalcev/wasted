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
        public static string ReadString(string filePath)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filename = Path.Combine(path, filePath);

            return File.ReadAllText(filename);
        }

        public static void WriteString(string filePath, string content)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filename = Path.Combine(path, filePath);

            File.WriteAllText(filename, content);
        }
    }
}

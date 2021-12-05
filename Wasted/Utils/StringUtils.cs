using System.Linq;

namespace Wasted.Utils
{
    /// <summary>
    /// Utility functions for working with strings
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Checks whether any of given string is null or empty
        /// </summary>
        /// <returns>'false' if any of given string is null or empty. 'true' if all strings are not null or empty</returns>
        public static bool AnyNullOrEmpty(params string[] strings)
        {
            return strings.Any(string.IsNullOrEmpty);
        }
    }
}
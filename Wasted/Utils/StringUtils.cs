using System.Linq;

namespace Wasted.Utils
{
    public static class StringUtils
    {
        public static bool AnyNullOrEmpty(params string[] strings)
        {
            return strings.Any(string.IsNullOrEmpty);
        }
    }
}
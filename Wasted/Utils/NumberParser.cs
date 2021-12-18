namespace Wasted.Utils
{
    public static class NumberParser
    {
        public static bool IsValidNumberString(string s)
        {
            return string.IsNullOrEmpty(s) ||
                (double.TryParse(s, out double number) && number >= 0);

        }

        public static long ParseLong(string s)
        {
            if (long.TryParse(s, out long number) && number >= 0)
                return number;
            return -1;
        }
    }
}
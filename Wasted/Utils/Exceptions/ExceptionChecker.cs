using System;
using System.Linq;

namespace Wasted.Utils.Exceptions
{
    public static class ExceptionChecker
    {
        public static void CheckValidParams(params string[] values)
        {
            if (values.Any(string.IsNullOrEmpty))
                throw new ArgumentNullException();
        }
    }
}
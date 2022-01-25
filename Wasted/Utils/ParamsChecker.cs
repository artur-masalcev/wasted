using System;
using System.Linq;

namespace Wasted.Utils.Exceptions
{
    public static class ParamsChecker
    {
        public static bool ValidParams(params string[] values)
        {
            return values.All(val => !string.IsNullOrEmpty(val));
        }
    }
}
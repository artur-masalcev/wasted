using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Utils
{
    public class UserProvider
    {
        public static bool UserExists(string name, string password)
        {
            return true;
        }

        public static int GetUserID(string name, string password)
        {
            return 8080;
        }
    }
}

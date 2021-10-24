using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Utils
{
    public class UserProvider
    {
        public static bool UserExists(string name, string password)
        {
            return name != null && App.Users.ContainsKey(name) &&
                password != null && App.Users[name].ContainsKey(password);
        }

        public static int GetUserID(string name, string password)
        {
            return App.Users[name][password];
        }
    }
}

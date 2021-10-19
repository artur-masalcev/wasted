using System;
using System.Collections.Generic;
using System.Text;
using Wasted.Utils;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserService))]
namespace Wasted.Utils
{
    public class UserService : IUserService
    {
        private int UserID;
        public int GetUserID()
        {
            return UserID;
        }

        public void SetUserID(int UserID)
        {
            this.UserID = UserID;
        }
    }
}

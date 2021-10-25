using System;
using System.Collections.Generic;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserService))]
namespace Wasted.Utils
{
    public class UserService : IUserService
    {
        private User user;
        public User GetUser()
        {
            return user;
        }

        public void SetUser(User user)
        {
            this.user = user;
        }
    }
}

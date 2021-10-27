using System;
using System.Collections.Generic;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserService))]
namespace Wasted.Utils
{
    public class UserService
    {
        public User CurrentUser { get; set; }
    }
}

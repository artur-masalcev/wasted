using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserDetailsService))]

namespace Wasted.Utils.Services
{
    public class UserDetailsService
    {
        public int UserId { get; set; } //TODO: convert to long
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Location UserLocation { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService))]

namespace Wasted.Utils.Services
{
    public class DataService
    {
        public Func<User> UserGettingFunction;
        public void UpdateUserInfo()
        {
            CurrentUser = UserGettingFunction.Invoke();
        }
        public User CurrentUser { get; set; }
        public Location UserLocation { get; set; }


        public List<OrderDeal> CartDeals { get; set; } = new List<OrderDeal>();
        public List<OrderDeal> OrderedDeals { get; set; } = new List<OrderDeal>();
    }
}
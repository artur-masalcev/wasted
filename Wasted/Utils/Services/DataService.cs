using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAPI.DTO;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(DataService))]
namespace Wasted.Utils
{
    public class DataService
    {

        public User CurrentUser { get; set; }
        private Lazy<Task<List<FoodPlace>>> LazyAllFoodPlaces = new Lazy<Task<List<FoodPlace>>>( () => Task.Run(async () =>
            await DataProvider.GetData<List<FoodPlace>>(DataProvider.FoodPlacesEnd))
        );
        public List<FoodPlace> AllFoodPlaces => LazyAllFoodPlaces.Value.Result;
        
        private Lazy<Task<List<Deal>>> LazyAllDeals = new Lazy<Task<List<Deal>>>(() => Task.Run(async () => 
            await DataProvider.GetData<List<Deal>>(DataProvider.DealsEnd))     
        );

        public List<Deal> AllDeals => LazyAllDeals.Value.Result;

        private Lazy<Task<List<ClientUser>>> LazyClientUsers = new Lazy<Task<List<ClientUser>>>(
            () => Task.Run(async () => 
                await DataProvider.GetData<List<ClientUser>>(DataProvider.ClientUsersEnd))
        );
        public List<ClientUser> ClientUsers => LazyClientUsers.Value.Result;

        private Lazy<Task<List<UserPlace>>> LazyPlaceUsers = new Lazy<Task<List<UserPlace>>>(
            () => Task.Run(async () =>
                await DataProvider.GetData<List<UserPlace>>(DataProvider.PlaceUsersEnd))
        );
        //                username
        public List<UserPlace> PlaceUsers => LazyPlaceUsers.Value.Result;
        
        public Location UserLocation { get; set; }
    }
}

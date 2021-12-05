using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public List<FoodPlace> AllFoodPlaces => Task.Run(async () =>
            await DataProvider.GetData<List<FoodPlace>>(DataProvider.FoodPlacesEnd)).Result;

        private Lazy<Task<List<Deal>>> LazyAllDeals = new Lazy<Task<List<Deal>>>(() => Task.Run(async () =>
            await DataProvider.GetData<List<Deal>>(DataProvider.DealsEnd))
        );

        public List<Deal> AllDeals => LazyAllDeals.Value.Result;

        public List<ClientUser> ClientUsers => Task.Run(async () =>
            await DataProvider.GetData<List<ClientUser>>(DataProvider.ClientUsersEnd)).Result;
        
        public List<PlaceUser> PlaceUsers => Task.Run(async () =>
            await DataProvider.GetData<List<PlaceUser>>(DataProvider.PlaceUsersEnd)).Result;

        public Location UserLocation { get; set; }

        public ClientUser GetClientUser(string username, string password)
        {
            return Task.Run(async () => await DataProvider.GetData<ClientUser>(DataProvider.ClientUserEnd(username, password))).Result;
        }

        public PlaceUser GetPlaceUser(string username, string password) //TODO: fix name mismatches
        {
            return Task.Run(async () => await DataProvider.GetData<PlaceUser>(DataProvider.PlaceUserEnd(username, password))).Result;
        }

        public List<FoodPlaceType> GetFoodPlaceTypes => Task.Run(async () =>
            await DataProvider.GetData<List<FoodPlaceType>>(DataProvider.PlaceTypeEnd)).Result;
    }
}

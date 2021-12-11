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

        public List<FoodPlace> AllFoodPlaces => Task.Run(async () =>
            await DataProvider.GetData<List<FoodPlace>>(DataProvider.FoodPlacesEnd)).Result;

        public List<Deal> AllDeals => Task.Run(async () =>
            await DataProvider.GetData<List<Deal>>(DataProvider.DealsEnd)).Result;

        public List<ClientUser> ClientUsers => Task.Run(async () =>
            await DataProvider.GetData<List<ClientUser>>(DataProvider.ClientUsersEnd)).Result;

        public List<PlaceUser> PlaceUsers => Task.Run(async () =>
            await DataProvider.GetData<List<PlaceUser>>(DataProvider.PlaceUsersEnd)).Result;

        public Location UserLocation { get; set; }

        public ClientUser GetClientUser(string username, string password)
        {
            return Task.Run(async () =>
                await DataProvider.GetData<ClientUser>(DataProvider.ClientUserEnd(username, password))).Result;
        }

        public PlaceUser GetPlaceUser(string username, string password) //TODO: fix name mismatches
        {
            return Task.Run(async () =>
                await DataProvider.GetData<PlaceUser>(DataProvider.PlaceUserEnd(username, password))).Result;
        }

        public List<FoodPlaceType> GetFoodPlaceTypes => Task.Run(async () =>
            await DataProvider.GetData<List<FoodPlaceType>>(DataProvider.PlaceTypeEnd)).Result;
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted
{
    public partial class App : Application
    {
        public static List<FoodPlace> AllFoodPlaces { get; set; }
        public static List<Deal> AllDeals { get; set; }
        //                       username
        public static Dictionary<string, UserClient> ClientUsers { get; set; }
        public static Dictionary<string, UserPlace> PlaceUsers { get; set; }

        private HttpClient client;

        public App()
        {
            DependencyService.Register<UserService>();

            Task.Run(GetData).Wait();
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces, AllDeals);

            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public void GetData()
        {
            client = new HttpClient();
            AllFoodPlaces = DummyDataProvider.GetData<List<FoodPlace>>(client, DummyDataProvider.FoodPlacesEnd).Result;
            AllDeals = DummyDataProvider.GetData<List<Deal>>(client, DummyDataProvider.DealsEnd).Result;
            ClientUsers = DummyDataProvider.GetData<Dictionary<string, UserClient>>(client, DummyDataProvider.ClientUsersEnd).Result;
            PlaceUsers = DummyDataProvider.GetData<Dictionary<string, UserPlace>>(client, DummyDataProvider.PlaceUsersEnd).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(() => WriteData()).Wait();
        }

        public void WriteData()
        {
            DummyDataProvider.WriteData(client, AllFoodPlaces, DummyDataProvider.FoodPlacesEnd).Wait();
            DummyDataProvider.WriteData(client, AllDeals, DummyDataProvider.DealsEnd).Wait();
            DummyDataProvider.WriteData(client, ClientUsers, DummyDataProvider.ClientUsersEnd).Wait();
            DummyDataProvider.WriteData(client, PlaceUsers, DummyDataProvider.PlaceUsersEnd).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

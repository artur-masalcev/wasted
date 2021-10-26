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
            AllFoodPlaces = DummyDataProvider.GetFoodPlaces(client).Result;
            AllDeals = DummyDataProvider.GetDeals(client).Result;
            ClientUsers = DummyDataProvider.GetClientUsers(client).Result;
            PlaceUsers = DummyDataProvider.GetPlaceUsers(client).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(() => WriteData()).Wait();
        }

        public void WriteData()
        {
            DummyDataProvider.WriteFoodPlaces(client, AllFoodPlaces).Wait();
            DummyDataProvider.WriteDeals(client, AllDeals).Wait();
            DummyDataProvider.WriteClientUsers(client, ClientUsers).Wait();
            DummyDataProvider.WritePlaceUsers(client, PlaceUsers).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

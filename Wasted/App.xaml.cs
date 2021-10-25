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
        public static Dictionary<string, User> Users { get; set; }

        private HttpClient client;

        public App()
        {
            DependencyService.Register<UserService>();

            Task.Run(() => GetData()).Wait();
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces, AllDeals);

            InitializeComponent();

            MainPage = new LoginPage();
        }

        public void GetData()
        {
            client = new HttpClient();
            AllFoodPlaces = DummyDataProvider.GetFoodPlaces(client).Result;
            AllDeals = DummyDataProvider.GetDeals(client).Result;
            Users = DummyDataProvider.GetUsers(client).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(() => WriteData()).Wait();
        }

        public void WriteData()
        {
            DummyDataProvider.WriteFoodPlaces(client, AllFoodPlaces).Wait();
            DummyDataProvider.WriteDeals(client, AllDeals).Wait();
            DummyDataProvider.WriteUsers(client, Users).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

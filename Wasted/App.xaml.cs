using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Wasted.Dummy_API;
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

        //                     UserID FoodPlaceID  Given rating
        public static Dictionary<int, Dictionary<int, int>> Ratings { get; set; }

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
            Ratings = DummyDataProvider.GetRatings(client).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(() => WriteData()).Wait();
            
            //TODO: write data to API
        }

        public void WriteData()
        {
            DummyDataProvider.WriteFoodPlaces(client, AllFoodPlaces).Wait();
            DummyDataProvider.WriteDeals(client, AllDeals).Wait();
            DummyDataProvider.WriteRatings(client, Ratings).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

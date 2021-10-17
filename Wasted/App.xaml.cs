using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Wasted.Dummy_API;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;

namespace Wasted
{
    public partial class App : Application
    {
        public static List<FoodPlace> AllFoodPlaces { get; set; }
        public static List<Deal> AllDeals { get; set; }

        public static int UserID => 8080;

        //                     UserID FoodPlaceID  Given rating
        public static Dictionary<int, Dictionary<int, int>> Ratings { get; set; }

        private HttpClient client;

        public App()
        {
            Task.Run(() => { GetData(); }).Wait();
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces, AllDeals);

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
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
            //TODO: write data to API
        }

        protected override void OnResume()
        {
        }
    }
}

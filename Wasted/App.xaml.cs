using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Wasted.Dummy_API;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;

namespace Wasted
{
    public partial class App : Application
    {
        public static List<FoodPlace> AllFoodPlaces { get; set; } = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
        public static List<Deal> AllDeals { get; set; } = new List<Deal>(DummyDataProvider.GetDealsList());

        public static int UserID = 8080;

        //               FoodPlaceID  Given rating
        public static Dictionary<int, int> ratings = DummyDataProvider.GetRatingsDictionary();

        public App()
        {
            InitializeComponent();
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces, AllDeals);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DummyDataProvider.WriteFoodPlacesList();
            DummyDataProvider.WriteDealsList();
        }

        protected override void OnResume()
        {
        }
    }
}

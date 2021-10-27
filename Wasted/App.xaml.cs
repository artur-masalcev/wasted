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
        private DataService service;
        private DummyDataProvider provider;

        public App()
        {
            service = DependencyService.Get<DataService>();
            provider = new DummyDataProvider();
            Task.Run(GetData).Wait();
            HashMaps.AddFoodPlacesToDeals();

            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public void GetData()
        {
            service.AllFoodPlaces = provider.GetData<List<FoodPlace>>(DummyDataProvider.FoodPlacesEnd).Result;
            service.AllDeals = provider.GetData<List<Deal>>(DummyDataProvider.DealsEnd).Result;
            service.ClientUsers = provider.GetData<Dictionary<string, UserClient>>(DummyDataProvider.ClientUsersEnd).Result;
            service.PlaceUsers = provider.GetData<Dictionary<string, UserPlace>>(DummyDataProvider.PlaceUsersEnd).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(() => WriteData()).Wait();
        }

        public void WriteData()
        {
            provider.WriteData(service.AllFoodPlaces, DummyDataProvider.FoodPlacesEnd).Wait();
            provider.WriteData(service.AllDeals, DummyDataProvider.DealsEnd).Wait();
            provider.WriteData(service.ClientUsers, DummyDataProvider.ClientUsersEnd).Wait();
            provider.WriteData(service.PlaceUsers, DummyDataProvider.PlaceUsersEnd).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

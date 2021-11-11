using System.Collections.Generic;
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
        private DataProvider provider;

        public App()
        {
            service = DependencyService.Get<DataService>();
            provider = new DataProvider();
            Task.Run(GetData).Wait();
            BusinessUtils.AddFoodPlacesToDeals();

            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        private void GetData()
        {
            service.AllFoodPlaces = provider.GetData<List<FoodPlace>>(DataProvider.FoodPlacesEnd).Result;
            service.AllDeals = provider.GetData<List<Deal>>(DataProvider.DealsEnd).Result;
            service.ClientUsers = provider.GetData<Dictionary<string, UserClient>>(DataProvider.ClientUsersEnd).Result;
            service.PlaceUsers = provider.GetData<Dictionary<string, UserPlace>>(DataProvider.PlaceUsersEnd).Result;
        }

        protected override void OnSleep()
        {
            Task.Run(WriteData).Wait();
        }

        private void WriteData()
        {
            provider.WriteData(service.AllFoodPlaces, DataProvider.FoodPlacesEnd).Wait();
            provider.WriteData(service.AllDeals, DataProvider.DealsEnd).Wait();
            provider.WriteData(service.ClientUsers, DataProvider.ClientUsersEnd).Wait();
            provider.WriteData(service.PlaceUsers, DataProvider.PlaceUsersEnd).Wait();
        }

        protected override void OnResume()
        {
        }
    }
}

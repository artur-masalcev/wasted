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
        public App()
        {
            service = DependencyService.Get<DataService>();
            BusinessUtils.AddFoodPlacesToDeals();

            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnSleep()
        {
            Task.Run(WriteData).Wait();
        }

        private void WriteData()
        {
            Task.WhenAll(
                DataProvider.WriteData(service.AllFoodPlaces, DataProvider.FoodPlacesEnd),
                DataProvider.WriteData(service.AllDeals, DataProvider.DealsEnd),
                DataProvider.WriteData(service.ClientUsers, DataProvider.ClientUsersEnd),
                DataProvider.WriteData(service.PlaceUsers, DataProvider.PlaceUsersEnd)
            );
        }

        protected override void OnResume()
        {
        }
    }
}

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
    }
}

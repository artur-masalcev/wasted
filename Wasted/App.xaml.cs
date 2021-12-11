using Wasted.Dummy_API;
using Xamarin.Forms;

namespace Wasted
{
    public partial class App : Application
    {
        public App()
        {
            BusinessUtils.AddFoodPlacesToDeals();
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}

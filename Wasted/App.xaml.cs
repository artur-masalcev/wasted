using Wasted.Pages.Login;
using Xamarin.Forms;

namespace Wasted
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
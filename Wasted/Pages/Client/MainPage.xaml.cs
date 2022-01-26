using Wasted.Utils;
using Wasted.Utils.Services;
using Xamarin.Forms;

namespace Wasted.Pages.Client
{
    /// <summary>
    /// Core page of the app, manages transitioning between other screens
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            UserDetailsService detailsService = DependencyService.Get<UserDetailsService>();
            detailsService.UserLocation = LocationUtils.GetLocation(this).Result;
            SelectedTabColor = Color.Black;
        }
    }
}
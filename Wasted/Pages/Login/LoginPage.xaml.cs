using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wasted.Pages.Place;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly CurrentUserService _service;

        private string UserName => UsernameEntry.Text;
        private string UserPassword => PasswordEntry.Text;

        public LoginPage()
        {
            _service = DependencyService.Get<CurrentUserService>();
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                Func<User>[] loginMethods = {LoginPlaceUser, LoginClientUser};
                User loggedUser = loginMethods.Select(login => login.Invoke())
                    .FirstOrDefault(user => user != null);
                if (loggedUser != null)
                {
                    loggedUser.PushPage(this);
                }
                else
                {
                    DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
                }
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private PlaceUser LoginPlaceUser() //TODO: probably move UserName and UserPassword outside properties
        {
            PlaceUser placeUser = DataProvider.GetPlaceUser(UserName, UserPassword); //TODO: do user specific DI
            if (placeUser != null)
                placeUser.Location = GetLocation().Result;
            return placeUser;
        }

        private ClientUser LoginClientUser()
        {
            return DataProvider.GetClientUser(UserName, UserPassword);
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(UserName, UserPassword);
        }

        /// <summary>
        /// Gets user's location
        /// </summary>
        private async Task<Location> GetLocation()
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync() ??
                           await Geolocation.GetLocationAsync(
                               new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(5))
                           );
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            return location;
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserAccountTypePage());
        }
    }
}
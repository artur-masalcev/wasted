using System;
using System.Threading.Tasks;
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
                Tuple<User, Func<User>> userDetails = GetUserDetails(UserName, UserPassword); //TODO: think when to pass parameters and when use global
                User user = userDetails.Item1;
                Func<User> userGettingFunction = userDetails.Item2;
                if (user != null)
                {
                    Location userLocation = GetLocation().Result;
                    _service.CurrentUser = user; //Sets user for whole app
                    _service.UserGettingFunction = userGettingFunction;
                    _service.UserLocation = userLocation;
                    user.PushPage(this); //Creates appropriate page
                }
                else
                {
                    DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
                }
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(UserName, UserPassword);
        }

        private Tuple<User, Func<User>> GetUserDetails(string username, string password)
        {
            User user = DataProvider.GetPlaceUser(username, password);
            if (user != null) //TODO: rewrite with optional if exists
            {
                return new Tuple<User, Func<User>>(user, () => DataProvider.GetPlaceUser(username, password));
            }

            user = DataProvider.GetClientUser(username, password);
            return user != null ?
                new Tuple<User, Func<User>>(user, () => DataProvider.GetClientUser(username, password)) :
                null;
        }

        /// <summary>
        /// Gets user's location
        /// </summary>
        private async Task<Location> GetLocation()
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(
                        new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(5)));
                }
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
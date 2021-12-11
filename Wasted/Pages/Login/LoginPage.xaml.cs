using System;
using System.Threading.Tasks;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
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
        private DataService service;
        public LoginPage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        private Tuple<User, Func<User>> GetUserDetails(string username, string password)
        {
            User user = service.GetPlaceUser(username, password);
            if (user != null)
                return new Tuple<User, Func<User>>(user, () => service.GetPlaceUser(username, password));
            user = service.GetClientUser(username, password);
            if (user != null)
                return new Tuple<User, Func<User>>(user, () => service.GetClientUser(username, password));
            return null;
        }

        private void SubmitUserData(string username, string password)
        {
            ExceptionChecker.CheckValidParams(username, password);
            Tuple<User, Func<User>> userDetails = GetUserDetails(username, password);
            User user = userDetails.Item1;
            Func<User> userGettingFunction = userDetails.Item2;
            if (user != null)
            {
                Location userLocation = GetLocation().Result;
                service.CurrentUser = user; //Sets user for whole app
                service.UserGettingFunction = userGettingFunction;
                service.UserLocation = userLocation;
                user.PushPage(this); //Creates appropriate page
            }
            else
            {
                DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
            }
        }
        
        private void LoginClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(
                () => SubmitUserData(UsernameEntry.Text, PasswordEntry.Text),
                this
            );
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
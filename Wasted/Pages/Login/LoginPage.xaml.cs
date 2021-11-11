using System;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages.Login;
using Wasted.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
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

        private async Task SubmitUserData(string username, string password)
        {

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) throw new ArgumentNullException();

            bool isClient = service.ClientUsers.ContainsKey(username);
            bool isPlace = service.PlaceUsers.ContainsKey(username);
            
            if (isClient || isPlace)
            {
                User user = isClient ? (User)service.ClientUsers[username] : service.PlaceUsers[username];
                if (user.Password == password)
                {
                    Location userLocation = GetLocation().Result;
                    if (userLocation == null)
                        return;
                    
                    DataService dataService = DependencyService.Get<DataService>();
                    dataService.CurrentUser = user; //Sets user for whole app
                    dataService.UserLocation = userLocation;
                    user.PushPage(this); //Creates appropriate page
                }
                else
                {
                    await DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
                }
            }
            else
            {
                await DisplayAlert("", "Username does not exist. Try Again.", "OK");
            }
        }
        
        private async void LoginClicked(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;
                await SubmitUserData(username, password);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("", "Please fill all fields", "OK");
            }
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
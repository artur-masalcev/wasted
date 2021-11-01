using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages;
using Wasted.Pages.Login;
using Wasted.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
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
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            string userName = Username.Text;
            string userPassword = Password.Text;
            bool isClient = service.ClientUsers.ContainsKey(userName);
            bool isPlace = service.PlaceUsers.ContainsKey(userName);
            if (isClient || isPlace)
            {
                User user = isClient ? (User)service.ClientUsers[userName] : service.PlaceUsers[userName];
                if (user.Password == userPassword)
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
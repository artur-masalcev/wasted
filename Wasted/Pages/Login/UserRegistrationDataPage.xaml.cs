using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages.Login;
using Wasted.Utils.Exceptions;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Wasted.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationDataPage : ContentPage
    {
        private DataService service;
        public UserRegistrationDataPage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void SubmitUserData(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) throw new ArgumentNullException();

            bool isClient = service.ClientUsers.ContainsKey(username);
            bool isPlace = service.PlaceUsers.ContainsKey(username);
            if (!isClient && !isPlace)
            {
                DataService dataService = DependencyService.Get<DataService>();
                User user = dataService.CurrentUser;
                user.Username = username;
                user.Password = password;
                Navigation.PushAsync(new UserRegistrationDeliveryPage());
            }
            else
            {
                throw new UserAlreadyExistsException();
            }
        }
        
        private void NextClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text ?? "";
            string password = PasswordEntry.Text ?? "";
            string repeatedPassword = RepeatedPasswordEntry.Text ?? "";

            if (password.Equals(repeatedPassword))
            {
                try
                {
                    SubmitUserData(username, password);
                }
                catch (ArgumentNullException)
                {
                    DisplayAlert("", "Please fill all fields", "OK");
                }
                catch (UserAlreadyExistsException)
                {
                    DisplayAlert("", "User with this username already exists", "OK");
                }
            }
            else
            {
                DisplayAlert("", "Passwords do not match.", "OK");
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            string userName = Username.Text;
            string userPassword = Password.Text;

            if (App.Users.ContainsKey(userName))
            {
                User user = App.Users[userName];
                IUserService userService = DependencyService.Get<IUserService>();
                userService.SetUser(user); //Sets user id for whole app

                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
            }
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new UserRegistrationPopup());
        }
    }
}
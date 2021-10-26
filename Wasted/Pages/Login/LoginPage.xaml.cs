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

            bool isClient = App.ClientUsers.ContainsKey(userName);
            bool isPlace = App.PlaceUsers.ContainsKey(userName);
            if (isClient || isPlace)
            {
                User user = isClient ? (User)App.ClientUsers[userName] : App.PlaceUsers[userName];
                IUserService userService = DependencyService.Get<IUserService>();
                userService.SetUser(user); //Sets user for whole app

                user.PushPage(this); //Creates appropriate page
            }
            else
            {
                await DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
            }
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserAccountTypePage());
        }
    }
}
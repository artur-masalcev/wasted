using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string userName = "";
            string userPassword = "";

            if (UserProvider.UserExists(userName, userPassword))
            {
                int userID = UserProvider.GetUserID(userName, userPassword);
                IUserService userService = DependencyService.Get<IUserService>();
                userService.SetUserID(userID);

                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
            }
        }
    }
}
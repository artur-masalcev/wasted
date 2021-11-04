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
        }

        private void NextClicked(object sender, EventArgs e)
        {
            string userName = Username.Text ?? "";
            string userPassword = Password.Text ?? "";
            string repeatedPassword = RepeatedPassword.Text ?? "";

            if (userPassword.Equals(repeatedPassword))
            {
                bool isClient = service.ClientUsers.ContainsKey(userName);
                bool isPlace = service.PlaceUsers.ContainsKey(userName);
                if (!isClient && !isPlace)
                {
                    DataService dataService = DependencyService.Get<DataService>();
                    User user = dataService.CurrentUser;
                    user.Username = userName;
                    user.Password = userPassword;
                    Navigation.PushAsync(new UserRegistrationDeliveryPage());
                }
                else
                {
                    DisplayAlert("", "Username already exists. Try another username.", "OK");
                }
            }
            else
            {
                DisplayAlert("", "Passwords do not match.", "OK");
            }
        }
    }
}
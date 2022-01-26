using System;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationDataPage : ContentPage
    {
        private readonly User _user;
        public UserRegistrationDataPage(User user)
        {
            InitializeComponent();
            _user = user;
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void NextClicked(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text ?? "";
            string repeatedPassword = RepeatedPasswordEntry.Text ?? "";
            string username = UsernameEntry.Text;

            if (password.Equals(repeatedPassword))
            {
                if (ParamsChecker.ValidParams(username, password))
                    SubmitUserData(username, password);
                else
                    this.DisplayFillFieldsAlert();
            }
            else
            {
                DisplayAlert("", "Passwords do not match", "OK");
            }
        }

        private void SubmitUserData(string username, string password)
        {
            bool existsUser = DataProvider.GetUser(username, password) != null;

            if (!existsUser)
            {
                _user.Username = username;
                _user.Password = password;
                Navigation.PushAsync(new UserRegistrationDeliveryPage(_user));
            }
            else
            {
                DisplayAlert("", "User with this username already exists", "OK");
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
using System;
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
        public UserRegistrationDataPage()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void SubmitUserData(string username, string password)
        {
            ExceptionChecker.CheckValidParams(username, password);

            bool existsUser = (DataProvider.GetPlaceUser(username, password) ??
                               (User) DataProvider.GetClientUser(username, password)) != null;

            if (!existsUser)
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
            string password = PasswordEntry.Text ?? "";
            string repeatedPassword = RepeatedPasswordEntry.Text ?? "";

            if (password.Equals(repeatedPassword))
            {
                ExceptionHandler.WrapFunctionCall(
                    () => SubmitUserData(UsernameEntry.Text, password),
                    this
                );
            }
            else
            {
                DisplayAlert("", "Passwords do not match", "OK");
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
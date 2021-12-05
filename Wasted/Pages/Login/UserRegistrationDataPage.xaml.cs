using System;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages.Login;
using Wasted.Utils.Exceptions;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

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
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void SubmitUserData(string username, string password)
        {
            ExceptionChecker.CheckValidParams(username, password);

            bool existsUser = (service.GetPlaceUser(username, password) ??
                                (User) service.GetClientUser(username, password)) != null;

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
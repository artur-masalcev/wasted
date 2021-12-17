using System;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountTypePage : ContentPage
    {
        public CurrentUserService CurrentUserService { get; set; }

        public UserAccountTypePage()
        {
            InitializeComponent();
            CurrentUserService = DependencyService.Get<CurrentUserService>();
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
            CurrentUserService.CurrentUser = new ClientUser();
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserRegistrationDataPage());
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }

        private void OnUserTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Value)
            {
                case "User":
                    CurrentUserService.CurrentUser = new ClientUser();
                    break;

                case "FoodPlace":
                    CurrentUserService.CurrentUser = new PlaceUser();
                    break;
            }
        }
    }
}
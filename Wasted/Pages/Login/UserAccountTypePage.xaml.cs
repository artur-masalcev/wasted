using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, Func<User>> _userDictionary = new Dictionary<string, Func<User>>
        {
            {"User", () => new ClientUser()},
            {"FoodPlace", () => new PlaceUser()}
        };

        public UserAccountTypePage()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            User user = _userDictionary[(string)RadioButton.Value].Invoke();
            Navigation.PushAsync(new UserRegistrationDataPage(user));
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
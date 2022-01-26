using System;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationDeliveryPage : ContentPage
    {
        private string UserName => NameEntry.Text;
        private string UserSurname => SurnameEntry.Text;
        private string UserCity => CityEntry.Text;
        private string UserAddress => AddressEntry.Text;

        private User _user;
        public UserRegistrationDeliveryPage(User user)
        {
            InitializeComponent(); //FOverride initialize component with custom page
            _user = user;
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void CreateClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                _user.Name = UserName;
                _user.Surname = UserSurname;
                _user.City = UserCity;
                _user.Address = UserAddress;

                _user.CreateUser();
                Navigation.PushAsync(new LoginPage());
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
           
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(UserName, UserSurname, UserCity, UserAddress);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
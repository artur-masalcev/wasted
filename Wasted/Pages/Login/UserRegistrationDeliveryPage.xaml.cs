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
        public UserRegistrationDeliveryPage()
        {
            InitializeComponent(); //FOverride initialize component with custom page
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void CreateClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                CurrentUserService currentUserService = DependencyService.Get<CurrentUserService>();
                User currentUser = currentUserService.CurrentUser;

                currentUser.Name = UserName;
                currentUser.Surname = UserSurname;
                currentUser.City = UserCity;
                currentUser.Address = UserAddress;

                currentUser.CreateUser();
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
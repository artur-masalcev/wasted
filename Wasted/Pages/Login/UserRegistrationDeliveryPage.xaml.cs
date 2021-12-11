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
        public UserRegistrationDeliveryPage()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void SubmitUserData(string userName, string userSurname, string userCity, string userAddress)
        {
            ExceptionChecker.CheckValidParams(userName, userSurname, userCity, userAddress);
            
            DataService dataService = DependencyService.Get<DataService>();
            User currentUser = dataService.CurrentUser;

            currentUser.Name = userName;
            currentUser.Surname = userSurname;
            currentUser.City = userCity;
            currentUser.Address = userAddress;
            
            currentUser.CreateUser();
            Navigation.PushAsync(new LoginPage());
        }
        
        private void CreateClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(
                () => SubmitUserData(NameEntry.Text, SurnameEntry.Text, CityEntry.Text, AddressEntry.Text),
                this
            );
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
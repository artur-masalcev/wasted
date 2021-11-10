using System;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
        }

        private void SubmitUserData(string userName, string userSurname, string userCity, string userAddress)
        {
            if (String.IsNullOrEmpty(userName) ||
                String.IsNullOrEmpty(userSurname) ||
                String.IsNullOrEmpty(userCity) ||
                String.IsNullOrEmpty(userAddress))
            {
                throw new ArgumentNullException();
            }
            
            DataService dataService = DependencyService.Get<DataService>();
            User currentUser = dataService.CurrentUser;

            currentUser.Name = userName;
            currentUser.Surname = userSurname;
            currentUser.City = userCity;
            currentUser.Address = userAddress;
            
            currentUser.CreateUser(dataService);
            Navigation.PushAsync(new LoginPage());
        }
        
        
        private void CreateClicked(object sender, EventArgs e)
        {
            try
            {
                SubmitUserData(NameEntry.Text, SurnameEntry.Text, CityEntry.Text, AddressEntry.Text);
            }
            catch (ArgumentNullException)
            {
                DisplayAlert("", "Please fill all fields", "OK");
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
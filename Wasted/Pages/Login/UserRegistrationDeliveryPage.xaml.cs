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

        private void CreateClicked(object sender, EventArgs e)
        {
            DataService dataService = DependencyService.Get<DataService>();
            User currentUser = dataService.CurrentUser;
            currentUser.Name = Name.Text;
            currentUser.Surname = Surname.Text;
            currentUser.City = City.Text;
            currentUser.Address = Address.Text;

            currentUser.CreateUser(dataService);
            Navigation.PushAsync(new LoginPage());
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
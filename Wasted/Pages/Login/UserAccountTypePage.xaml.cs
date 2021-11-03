using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountTypePage : ContentPage
    {
        enum UserType { User, FoodPlace}

        UserType SelectedUserType { get; set; }
        public DataService DataService { get; set; }

        public UserAccountTypePage()
        {
            InitializeComponent();
            DataService = DependencyService.Get<DataService>();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SelectedUserType = UserType.User;
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            Console.WriteLine(SelectedUserType);
            switch (SelectedUserType)
            {
                case UserType.User:
                {
                    DataService.CurrentUser = new UserClient();
                    Navigation.PushAsync(new UserRegistrationDataPage());
                    break;
                }
                case UserType.FoodPlace:
                {
                    DataService.CurrentUser = new UserPlace();
                    Navigation.PushAsync(new UserRegistrationDataPage());
                    break;
                }
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }

        private void OnUserTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Value) {
                case "User":
                    SelectedUserType = UserType.User;
                    break;

                case "FoodPlace":
                    SelectedUserType = UserType.FoodPlace;
                    break;
            }
        }
    }
}
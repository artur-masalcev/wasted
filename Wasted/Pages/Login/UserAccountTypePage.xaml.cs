using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountTypePage : ContentPage
    {
        public UserService UserService { get; set; }
        public UserAccountTypePage()
        {
            InitializeComponent();
            UserService = DependencyService.Get<UserService>();

        }

        private void PlaceClicked(object sender, EventArgs e)
        {
            UserService.CurrentUser = new UserPlace();
            Navigation.PushAsync(new UserRegistrationDataPage());
        }

        private void UserClicked(object sender, EventArgs e)
        {
            UserService.CurrentUser = new UserClient();
            Navigation.PushAsync(new UserRegistrationDataPage());
        }
    }
}
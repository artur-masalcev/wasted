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
        public DataService DataService { get; set; }
        public UserAccountTypePage()
        {
            InitializeComponent();
            DataService = DependencyService.Get<DataService>();

        }

        private void PlaceClicked(object sender, EventArgs e)
        {
            DataService.CurrentUser = new UserPlace();
            Navigation.PushAsync(new UserRegistrationDataPage());
        }

        private void UserClicked(object sender, EventArgs e)
        {
            DataService.CurrentUser = new UserClient();
            Navigation.PushAsync(new UserRegistrationDataPage());
        }
    }
}
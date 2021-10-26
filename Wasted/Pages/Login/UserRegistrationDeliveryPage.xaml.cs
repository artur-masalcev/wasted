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
    public partial class UserRegistrationDeliveryPage : ContentPage
    {
        public UserRegistrationDeliveryPage()
        {
            InitializeComponent();
        }

        private void CreateClicked(object sender, EventArgs e)
        {
            IUserService userService = DependencyService.Get<IUserService>();
            User currentUser = userService.GetUser();
            currentUser.Name = Name.Text;
            currentUser.Surname = Surname.Text;
            currentUser.City = City.Text;
            currentUser.Address = Address.Text;

            currentUser.CreateUser();
            Navigation.PushAsync(new LoginPage());
        }
    }
}
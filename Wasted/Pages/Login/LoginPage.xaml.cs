using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wasted.Pages.Place;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly UserDetailsService _detailsService;

        private string UserName => UsernameEntry.Text;
        private string UserPassword => PasswordEntry.Text;

        public LoginPage()
        {
            _detailsService = DependencyService.Get<UserDetailsService>();
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                User loggedUser = UsersProvider.GetUser(UserName, UserPassword);
                if (loggedUser != null)
                {
                    loggedUser.InitializePage(this);
                    _detailsService.UserId = loggedUser.Id;
                    _detailsService.UserName = loggedUser.Username;
                    _detailsService.UserPassword = loggedUser.Password;
                }
                else
                {
                    DisplayAlert("", "Username or password is incorrect. Try Again.", "OK");
                }
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(UserName, UserPassword);
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserAccountTypePage());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;

namespace Wasted.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationPopup : PopupPage
    {
        public UserRegistrationPopup()
        {
            InitializeComponent();
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            string userName = Username.Text;
            string userPassword = Password.Text;
            string repeatedPassword = RepeatedPassword.Text;

            if (userPassword.Equals(repeatedPassword))
            {
                if(!UserProvider.UserExists(userName, userPassword))
                {
                    var dictionary = new Dictionary<string, int>();
                    dictionary.Add(userPassword, App.Users.Count);
                    App.Users.Add(userName, dictionary);
                    PopupNavigation.Instance.PopAsync(true);
                }
                else
                {
                    DisplayAlert("", "Username already exists. Try another username.", "OK");
                }
            }
            else
            {
                DisplayAlert("", "Passwords do not match. ", "OK");
            }
        }
    }
}
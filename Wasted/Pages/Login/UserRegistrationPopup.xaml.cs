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
using Wasted.Dummy_API.Business_Objects;

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
                if(!App.Users.ContainsKey(userName))
                { 
                    App.Users.Add(userName,
                        new User(userName, userPassword));
                    PopupNavigation.Instance.PopAsync(true);
                }
                else
                {
                    DisplayAlert("", "Username already exists. Try another username.", "OK");
                }
            }
            else
            {
                DisplayAlert("", "Passwords do not match.", "OK");
            }
        }
    }
}
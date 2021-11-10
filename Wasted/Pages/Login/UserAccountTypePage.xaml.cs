﻿using System;
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
        public DataService DataService { get; set; }

        public UserAccountTypePage()
        {
            InitializeComponent();
            DataService = DependencyService.Get<DataService>();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true); // Put margin on iOS devices that have top notch
            DataService.CurrentUser = new UserClient();
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserRegistrationDataPage());
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
                    DataService.CurrentUser = new UserClient();
                    break;

                case "FoodPlace":
                    DataService.CurrentUser = new UserPlace();
                    break;
            }
        }
    }
}
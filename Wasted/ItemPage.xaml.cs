using System;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        public ItemsPage(Deal selectedDeal)
        {
            SelectedDeal = selectedDeal;

            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeViews();
        }

        public void InitializeViews()
        {
            content.BindingContext = SelectedDeal;
        }

        private void OnOrderClicked(object sender, EventArgs e)
        {
            if (SelectedDeal.Quantity > 0)
            {
                popupLoadingView.IsVisible = true;
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("No orders left!", new TimeSpan(3));
            }
        }

        private void ClosePopup()
        {
            popupLoadingView.IsVisible = false;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            ClosePopup();
            if (SelectedDeal.Quantity == stepper.Value)
            {
                ((StackLayout)stepper.Parent).Children.Remove(stepper);
                SelectedDeal.Quantity = 0;
            }
            else
            {
                SelectedDeal.Quantity -= (int)stepper.Value;
                stepper.Value = 0;
            }

            Acr.UserDialogs.UserDialogs.Instance.Toast("Order confirmed", new TimeSpan(3));
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedDeal.SelectedCount = (int)stepper.Value;
        }
    }
}
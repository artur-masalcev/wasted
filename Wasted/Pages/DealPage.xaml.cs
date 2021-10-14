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

        private void RemoveStepper()
        {
            ((StackLayout)stepper.Parent).Children.Remove(stepper);
        }

        /// <summary>
        /// Sets binding for xaml file. Removes deal stepper if it is already taken
        /// </summary>
        public void InitializeViews()
        {
            if (SelectedDeal.Quantity < 1)
            {
                RemoveStepper();
            }
            content.BindingContext = SelectedDeal;
        }

        /// <summary>
        /// Checks if there are available deals.
        /// </summary>
        private void OnOrderClicked(object sender, EventArgs e)
        {
            if (SelectedDeal.Quantity > 0)
            {
                orderView.IsVisible = true;
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("No orders left!", new TimeSpan(3));
            }
        }

        private void ClosePopup()
        {
            orderView.IsVisible = false;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Reduces deal count from deal by stepper value.
        /// If all deals are taken, stepper is removed from layout to prevent exceptions.
        /// </summary>
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            ClosePopup();
            bool selectedSomething = stepper.Value != 0;

            if (SelectedDeal.Quantity == stepper.Value)
            {
                RemoveStepper();
                SelectedDeal.Quantity = 0;
            }
            else
            {
                SelectedDeal.Quantity -= (int)stepper.Value;
                stepper.Value = 0;
            }

            if (selectedSomething)
                Acr.UserDialogs.UserDialogs.Instance.Toast("Order confirmed", new TimeSpan(3));
        }

        private void StepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedDeal.SelectedCount = (int)stepper.Value;
        }
    }
}
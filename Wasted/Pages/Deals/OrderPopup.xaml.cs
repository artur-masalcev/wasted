using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.FoodPlaceRatingSystem;
using Xamarin.Forms;

namespace Wasted.Pages.Deals
{
    public partial class OrderPopup : PopupPage
    {
        public Deal SelectedDeal { get; set; }

        private int selectedCount = 0;
        public int SelectedCount
        {
            get { return selectedCount; }
            set
            {
                selectedCount = value;
                OnPropertyChanged();
            }
        }
        public OrderPopup(Deal deal)
        {
            SelectedDeal = deal;
            InitializeComponent();
            orderView.BindingContext = SelectedDeal;
            countLabel.BindingContext = this;
        }

        /// <summary>
        /// Reduces deal count from deal by stepper value.
        /// </summary>
        public void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
            bool selectedSomething = stepper.Value != 0;

            if (SelectedDeal.Quantity == stepper.Value)
            {
                stepper.Maximum = 1; //Prevents crashing from invalid maximum - 0
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

        public void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
        }

        private void StepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedCount = (int)stepper.Value;
        }
    }
}

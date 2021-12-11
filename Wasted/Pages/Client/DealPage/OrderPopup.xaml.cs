using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;

namespace Wasted.Pages.Client.DealPage
{
    public partial class OrderPopup : PopupPage
    {
        public Deal SelectedDeal { get; set; }
        public int StepperDealQuantity => SelectedDeal.Quantity == 0 ? 1 : SelectedDeal.Quantity;

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
            BindingContext = this;
        }

        /// <summary>
        /// Reduces deal count from deal by stepper value.
        /// </summary>
        public void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
            SelectedDeal.Quantity -= (int) stepper.Value;
            DataProvider.UpdateDeal(SelectedDeal);
            
            bool selectedSomething = stepper.Value != 0;
            if (selectedSomething)
                UserDialogs.Instance.Toast("Order confirmed", new TimeSpan(3));
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

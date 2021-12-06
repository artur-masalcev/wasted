using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;

namespace Wasted.Pages.Deals
{
    public partial class OrderPopup : PopupPage
    {
        public Deal SelectedDeal { get; set; }
        private DataService service;
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
            service = DependencyService.Get<DataService>();
        }

        /// <summary>
        /// Reduces deal count from deal by stepper value.
        /// </summary>
        public void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
            SelectedDeal.Quantity -= (int) stepper.Value;
            
            bool selectedSomething = stepper.Value != 0;
            if (selectedSomething)
            {
                CartDeal cartDeal = new CartDeal(SelectedDeal, (int) stepper.Value, stepper.Value * SelectedDeal.DealCosts.CurrentCost);
                service.CartDeals.Add(cartDeal);
                // Navigates to the third tabbed page and closes the Deal Page
                MessagingCenter.Send<object, int>(this,"click",(int) NavigationPages.CART_PAGE);
                Application.Current.MainPage.Navigation.PopAsync();
                UserDialogs.Instance.Toast("Added to cart", new TimeSpan(3));
            }
                
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

using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;

namespace Wasted.Pages.Client.DealPage
{
    public partial class OrderPopup : PopupPage
    {
        public Deal SelectedDeal { get; set; }
        private DataService service;
        public int StepperDealQuantity => SelectedDeal.Quantity == 0 ? 1 : SelectedDeal.Quantity;

        private int _selectedCount = 0;

        public int SelectedCount
        {
            get => _selectedCount;
            set
            {
                _selectedCount = value;
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
            PopupNavigation.Instance.PopAsync(); // Close the popup
            SelectedDeal.Quantity -= (int) Stepper.Value;
            DataProvider.UpdateDeal(SelectedDeal);

            bool selectedSomething = Stepper.Value != 0;
            if (selectedSomething)
            {
                CartDeal cartDeal = new CartDeal(SelectedDeal, (int) Stepper.Value,
                    Stepper.Value * SelectedDeal.CurrentCost);
                service.CartDeals.Add(cartDeal);
                // Navigates to the third tabbed page and closes the Deal Page
                Navigation.PushAsync(new CartPage());
                Application.Current.MainPage.Navigation.PopAsync();
                UserDialogs.Instance.Toast("Added to cart", new TimeSpan(3));
            }
        }

        public void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
        }

        private void StepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedCount = (int) Stepper.Value;
        }
    }
}
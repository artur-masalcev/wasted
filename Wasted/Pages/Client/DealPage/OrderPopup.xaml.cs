using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;

namespace Wasted.Pages.Client.DealPage
{
    public partial class OrderPopup : PopupPage
    {
        public Deal SelectedDeal { get; set; }
        private readonly UserDetailsService _detailsService;
        public int StepperDealQuantity => SelectedDeal.Quantity == 0 ? 1 : SelectedDeal.Quantity;

        private int _selectedCount;

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
            _detailsService = DependencyService.Get<UserDetailsService>();
        }

        /// <summary>
        /// Reduces deal count from deal by stepper value.
        /// </summary>
        public void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
            SelectedDeal.Quantity -= (int) Stepper.Value;
            DealsProvider.UpdateDeal(SelectedDeal);

            bool selectedSomething = Stepper.Value != 0;
            if (selectedSomething)
            {
                OrderDeal cartDeal = new OrderDeal(
                    SelectedDeal.Id,
                    OrderStatus.InCart,
                    (int) Stepper.Value,
                    _detailsService.UserId,
                    SelectedDeal.PlaceUserId,
                    SelectedDeal.Title,
                    SelectedDeal.CurrentCost
                );
                OrdersProvider.CreateOrder(cartDeal);

                UserDialogs.Instance.Toast("Added to cart", new TimeSpan(3));
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
        }

        private void StepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedCount = (int) Stepper.Value;
        }
    }
}
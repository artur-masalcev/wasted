using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPlacePopup : PopupPage
    {
        public OrderDeal SelectedDeal { get; set; }

        public string DealTitle => SelectedDeal.Title;
        public int Quantity => SelectedDeal.Quantity;

        public string Message => $"Deal {DealTitle} (x{Quantity}) is done preparing";

        public OrderStatusPlacePopup(OrderDeal deal)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = OrderStatus.ReadyToPickUp;
            PopupNavigation.Instance.PopAsync();
        }
    }
}
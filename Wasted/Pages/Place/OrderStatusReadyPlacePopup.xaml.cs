using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusReadyPlacePopup : PopupPage
    {
        public OrderDeal SelectedOrder { get; set; }
        public string DealTitle => SelectedOrder.Title;
        public int Quantity => SelectedOrder.Quantity;

        public string Message => $"Set {DealTitle} (x{Quantity}) as done preparing?";

        public OrderStatusReadyPlacePopup(OrderDeal order)
        {
            InitializeComponent();
            SelectedOrder = order;
            BindingContext = this;
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedOrder.Status = OrderStatus.ReadyToPickUp;
            OrdersProvider.UpdateOrder(SelectedOrder);
            PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
        }
    }
}
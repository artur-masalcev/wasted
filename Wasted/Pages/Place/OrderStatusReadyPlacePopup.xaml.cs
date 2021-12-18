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
        public OrderDeal SelectedDeal { get; set; }
        public OrdersPage ParentPage { get; set; }
        public string DealTitle => SelectedDeal.Title;
        public int Quantity => SelectedDeal.Quantity;

        public string Message => $"Set {DealTitle} (x{Quantity}) as done preparing?";

        public OrderStatusReadyPlacePopup(OrderDeal deal, OrdersPage ordersPage)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
            ParentPage = ordersPage;
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = OrderStatus.ReadyToPickUp;
            DataProvider.UpdateOrders(new List<OrderDeal>{SelectedDeal});
            PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
        }
    }
}
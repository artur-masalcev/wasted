using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusClientPopup : PopupPage
    {
        public OrderDeal SelectedDeal { get; set; }
        public string DealTitle => SelectedDeal.Title;
        public int Quantity => SelectedDeal.Quantity;

        public string Message => $"Deal {DealTitle} (x{Quantity}) is received";

        public OrderStatusClientPopup(OrderDeal deal)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = OrderStatus.Received;
            DataProvider.UpdateOrders(new List<OrderDeal> {SelectedDeal});
            PopupNavigation.Instance.PopAsync();
        }
    }
}
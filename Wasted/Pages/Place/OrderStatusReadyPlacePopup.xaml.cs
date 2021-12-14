﻿using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
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

        public string Message => $"Deal {DealTitle} (x{Quantity}) is done preparing";

        public OrderStatusReadyPlacePopup(OrderDeal deal, OrdersPage ordersPage)
        {
            InitializeComponent();
            ParentPage = ordersPage;
            SelectedDeal = deal;
            BindingContext = this;
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = OrderStatus.ReadyToPickUp;
            DataProvider.UpdateOrders(new List<OrderDeal>{SelectedDeal});
            //_logger.LogInformation(LogEvents.OrderPrepared, "Order {OrderTitle} ({OrderId}) prepared.", SelectedDeal.Title, SelectedDeal.Id);
            DataProvider.UpdateOrders(new List<OrderDeal> {SelectedDeal});
            ParentPage.UpdateSummaryListView();
            PopupNavigation.Instance.PopAsync();
        }
    }
}
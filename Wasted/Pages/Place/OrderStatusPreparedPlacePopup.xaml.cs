﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class OrderStatusPreparedPlacePopup : PopupPage
    {
        public OrderDeal SelectedDeal { get; set; }

        public string DealTitle => SelectedDeal.Title;
        public int Quantity => SelectedDeal.Quantity;

        public string Message => $"Do you accept deal {DealTitle} (x{Quantity})?";


        public OrderStatusPreparedPlacePopup(OrderDeal deal)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
            ErrorLabel.IsVisible = false;
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (Time.Text != null)
                {
                    SelectedDeal.ExpectedFinishTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() +
                                                      (long) TimeSpan.FromMinutes(long.Parse(Time.Text))
                                                          .TotalMilliseconds;
                }

                SelectedDeal.Status = OrderStatus.Preparing;
                DataProvider.UpdateOrders(new List<OrderDeal> {SelectedDeal});
                ErrorLabel.IsVisible = false;
                PopupNavigation.Instance.PopAsync();
            }
            catch
            {
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
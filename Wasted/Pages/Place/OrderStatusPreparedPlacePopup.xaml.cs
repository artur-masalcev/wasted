using System;
using System.Collections.Generic;
using System.Configuration;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
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
        public OrderDeal SelectedOrder { get; set; }
        public string DealTitle => SelectedOrder.Title;
        public int Quantity => SelectedOrder.Quantity;

        public string Message => $"Do you accept deal {DealTitle} (x{Quantity})?";


        public OrderStatusPreparedPlacePopup(OrderDeal order)
        {
            InitializeComponent();
            SelectedOrder = order;
            BindingContext = this;
            ErrorLabel.IsVisible = false;
            On<iOS>().SetUseSafeArea(true);
        }

        private static bool UpdateFinishTime(string selectedTime, OrderDeal selectedDeal)
        {
            if (string.IsNullOrEmpty(selectedTime))
                return true;
            long minutes = NumberParser.ParseLong(selectedTime);
            if (minutes == -1)
                return false;
            selectedDeal.ExpectedFinishTime = DateTimeOffset.Now.AddMinutes(minutes).ToUnixTimeMilliseconds();
            return true;
        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (UpdateFinishTime(Time.Text, SelectedOrder))
            {
                ErrorLabel.IsVisible = false;

                SelectedOrder.Status = OrderStatus.Preparing;
                OrdersProvider.UpdateOrder(SelectedOrder);
                PopupNavigation.Instance.PopAsync();
            }
            else
            {
                ErrorLabel.IsVisible = true;
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
        }
    }
}
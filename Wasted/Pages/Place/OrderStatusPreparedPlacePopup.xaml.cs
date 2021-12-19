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
            if (UpdateFinishTime(Time.Text, SelectedDeal))
            {
                ErrorLabel.IsVisible = false;

                SelectedDeal.Status = OrderStatus.Preparing;
                DataProvider.UpdateOrders(new List<OrderDeal> {SelectedDeal});
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
using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Deals;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Wasted.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        private DataService service;
        public ItemsPage(Deal selectedDeal)
        {
            SelectedDeal = selectedDeal;
           
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = SelectedDeal;

            service = DependencyService.Get<DataService>();
        }

        /// <summary>
        /// Checks if there are available deals.
        /// </summary>
        private void OnOrderClicked(object sender, EventArgs e)
        {
            if (SelectedDeal.Quantity > 0)
            {
                PopupNavigation.Instance.PushAsync(new OrderPopup(SelectedDeal));
            }
            else
            {
                UserDialogs.Instance.Toast("No orders left!", new TimeSpan(3));
            }
        }
        
        /// <summary>
        /// Refreshes the page on scroll down.
        /// </summary>
        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            SelectedDeal = service.AllDeals.Find(deal => deal.Id == SelectedDeal.Id);
            BindingContext = SelectedDeal;
            refreshView.IsRefreshing = false;
        }
    }
}
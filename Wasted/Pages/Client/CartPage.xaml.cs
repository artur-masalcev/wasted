using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private DataService service;
        private bool init;
        

        public CartPage()
        {
            service = DependencyService.Get<DataService>();
        }

        /// <summary>
        /// Sets CartDealsCollectionView with CartDeals
        /// </summary>
        protected override void OnAppearing()
        {
            if (!init)
            {
                InitializeComponent();
                init = true;
            }

            base.OnAppearing();
            service = DependencyService.Get<DataService>();
            CartDealsCollectionView.ItemsSource = service.CartDeals;
            Total.Text = "Total " + setTotal() + " eur.";
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        /// <summary>
        /// Counts the total cost of all deals
        /// </summary>
        private double setTotal()
        {
            var allCartDeals = service.CartDeals;
            double total = 0;
            foreach (CartDeal deal in allCartDeals)
            {
                total += deal.Cost;
            }

            return total;
        }

        private void ClickedPurchase(object sender, EventArgs e)
        {
            UserDialogs.Instance.Toast("Purchased successfully", new TimeSpan(3));
        }
    }
}
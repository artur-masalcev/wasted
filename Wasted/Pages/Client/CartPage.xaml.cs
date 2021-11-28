using System;
using System.Collections.Generic;
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
        public CartDeal cartDeal { get; set; }
        private DataService service;

        public CartPage(Deal selectedDeal, int quantity, double cost)
        {
            cartDeal = new CartDeal(selectedDeal, quantity, "preparing", cost);

            InitializeComponent();

            service = DependencyService.Get<DataService>();

            service.CartDeals.Add(cartDeal);

            Total.Text = "Total " + setTotal() + " eur.";
        }

        public CartPage()
        {
            InitializeComponent();

            service = DependencyService.Get<DataService>();
            
            CartDealsCollectionView.ItemsSource = service.CartDeals;
            
            Total.Text = "Total " + setTotal() + " eur.";
        }

        /// <summary>
        /// Sets CartDealsCollectionView with CartDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CartDealsCollectionView.ItemsSource = service.CartDeals;
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
            
        }
    }
}
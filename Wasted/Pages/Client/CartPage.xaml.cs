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
        public List<CartDeal> CartDeals { get; set; }
        

        public CartPage()
        {
            InitializeComponent();
            service = DependencyService.Get<DataService>();
            CartDeals = service.CartDeals;
            CartDealsCollectionView.ItemsSource = CartDeals;
        }

        /// <summary>
        /// Sets CartDealsCollectionView with CartDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //CartDeals = service.CartDeals;
            CartDealsCollectionView.ItemsSource = null;
            CartDealsCollectionView.ItemsSource = CartDeals;
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

        private void Button_OnClicked(object sender, EventArgs e)
        {
            service.CartDeals.Add(new CartDeal(new Deal(title:"asf"), 1, 1));
            OnPropertyChanged("CartDeals");
        }
    }
}
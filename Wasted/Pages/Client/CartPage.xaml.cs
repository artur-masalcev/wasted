using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Wasted.Dummy_API.Business_Objects;
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
            CartDealsCollectionView.ItemsSource = null;
            CartDealsCollectionView.ItemsSource = CartDeals;
            Total.Text = "Total " + SetTotal() + " eur.";
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        /// <summary>
        /// Counts the total cost of all deals
        /// </summary>
        private double SetTotal()
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
            if (CartDeals.Any())
            {
                foreach (CartDeal cartDeal in service.CartDeals)
                {
                    service.OrderedDeals.Add(new OrderedDeal(cartDeal, "preparing"));
                }
            
                service.CartDeals = new List<CartDeal>();
                CartDeals = service.CartDeals;
                OnAppearing();
                
                UserDialogs.Instance.Toast("Purchased successfully", new TimeSpan(3));
            }
            else
            {
                UserDialogs.Instance.Toast("Cart is empty", new TimeSpan(3));
            }
        }
    }
}
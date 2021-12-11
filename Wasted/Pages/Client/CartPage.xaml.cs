using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private readonly DataService service;
        public List<OrderDeal> CartDeals { get; set; }

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
            Total.Text = "Total " + service.CartDeals.Sum(deal => deal.Cost) + " eur.";
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }
        

        private void ClickedPurchase(object sender, EventArgs e)
        {
            if (CartDeals.Any())
            {
                service.OrderedDeals = new List<OrderDeal>(service.CartDeals);
                service.CartDeals = new List<OrderDeal>();
                CartDeals = service.CartDeals;
                OnAppearing();
                UserDialogs.Instance.Toast("Purchased successfully", new TimeSpan(3));
                Navigation.PushAsync(new OrderStatusPage());
            }
            else
            {
                UserDialogs.Instance.Toast("Cart is empty", new TimeSpan(3));
            }
        }
    }
}
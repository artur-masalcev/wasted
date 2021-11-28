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
        public OrderedDeal orderedDeal { get; set; }
        private DataService service;

        public CartPage(Deal selectedDeal, int quantity, double cost)
        {
            orderedDeal = new OrderedDeal(selectedDeal, quantity, "preparing", cost);

            InitializeComponent();

            service = DependencyService.Get<DataService>();
            
            if (service.OrderedDeals == null)
            {
                service.OrderedDeals = new List<OrderedDeal>();
            }

            service.OrderedDeals.Add(orderedDeal);

            Total.Text = "Total " + setTotal() + " eur.";
        }

        public CartPage()
        {
            InitializeComponent();

            service = DependencyService.Get<DataService>();
            
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
            
            Total.Text = "Total " + setTotal() + " eur.";
        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        private double setTotal()
        {
            var allOrderedDeals = service.OrderedDeals;
            double total = 0;
            foreach (OrderedDeal deal in allOrderedDeals)
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
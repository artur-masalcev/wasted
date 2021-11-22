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

        public CartPage(Deal selectedDeal, int quantity)
        {
            orderedDeal = new OrderedDeal(selectedDeal, quantity, "preparing");

            InitializeComponent();

            service = DependencyService.Get<DataService>();
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (service.OrderedDeals == null)
            {
                service.OrderedDeals = new List<OrderedDeal>();
            }
            service.OrderedDeals.Add(orderedDeal);
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }
    }
}
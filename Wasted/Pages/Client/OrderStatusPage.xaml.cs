﻿using System;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage : ContentPage
    {
        private readonly DataService service;

        public OrderStatusPage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderedDealsCollectionView.ItemsSource = null;
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        private void OrderedDealsCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                OrderedDeal order = e.CurrentSelection.FirstOrDefault() as OrderedDeal;
                if (order.Status.Equals("Ready to pick up"))
                {
                    PopupNavigation.Instance.PushAsync(new OrderStatusClientPopup(order));
                }
            });
        }
    }
}
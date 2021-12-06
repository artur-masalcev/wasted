﻿using System;
using System.Collections.Generic;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage : ContentPage
    {
        private DataService service;

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
    }
}
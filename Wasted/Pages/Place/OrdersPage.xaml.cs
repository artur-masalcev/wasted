﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private readonly CurrentUserService _service;
        private List<OrderDeal> PlaceOrders { get; set; }
        public PlaceUser CurrentUser { get; set; }
        private IEnumerable<FoodPlace> _ownedPlaces;
        public IEnumerable<FoodPlace> OwnedPlaces
        {
            get => _ownedPlaces;
            set
            {
                _ownedPlaces = value;
                OnPropertyChanged();
            }
        }
        
        public OrdersPage()
        {
            _service = DependencyService.Get<CurrentUserService>();
            CurrentUser = (PlaceUser) _service.CurrentUser;
            OwnedPlaces = CurrentUser.OwnedPlaces;
            InitializeComponent();
        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PlaceOrders = DataProvider.GetPlaceOrders(CurrentUser.Id);
            OrdersCollectionView.ItemsSource = null;
            OrdersCollectionView.ItemsSource = PlaceOrders;
            UpdateSummaryListView();
        }

        public void UpdateSummaryListView()
        {
            SummaryListView.ItemsSource = from groupElement in
                    from order in PlaceOrders
                    group order by order.Status
                    into newGroup
                    select newGroup
                select new OrderStatusSummary(groupElement.Key, 
                    groupElement.Sum(elem => elem.Quantity));
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        private void OrdersCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                OrderDeal order = e.CurrentSelection.FirstOrDefault() as OrderDeal;
                switch (order.Status)
                {
                    case OrderStatus.Preparing:
                        PopupNavigation.Instance.PushAsync(new OrderStatusPlacePopup(order, this));
                        break;
                    case OrderStatus.WaitingForAcceptance:
                        PopupNavigation.Instance.PushAsync(new OrderStatusPreparedPlacePopup(order));
                        break;
                }
            });
        }
    }
}
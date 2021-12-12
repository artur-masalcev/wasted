using System;
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
        public PlaceUser CurrentUser { get; set; }
        private IEnumerable<FoodPlace> ownedPlaces;
        public IEnumerable<FoodPlace> OwnedPlaces
        {
            get => ownedPlaces;
            set
            {
                ownedPlaces = value;
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
            OrdersCollectionView.ItemsSource = null;
            OrdersCollectionView.ItemsSource = DataProvider.GetPlaceOrders(CurrentUser.Id);

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
                if (order.Status.Equals(OrderStatus.WaitingForAcceptance))
                {
                    PopupNavigation.Instance.PushAsync(new OrderStatusPreparedPlacePopup(order));
                }
                else if (order.Status.Equals(OrderStatus.Preparing))
                {
                    PopupNavigation.Instance.PushAsync(new OrderStatusReadyPlacePopup(order));
                }
            });
        }
    }
}
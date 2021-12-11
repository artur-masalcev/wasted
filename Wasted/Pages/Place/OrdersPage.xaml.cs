using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private readonly DataService _service;
        public PlaceUser CurrentUser { get; set; }
        private IEnumerable<FoodPlace> ownedPlaces;
        private HashSet<int> ownedPlaceIds;


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
            _service = DependencyService.Get<DataService>();
            CurrentUser = (PlaceUser) _service.CurrentUser;
            OwnedPlaces = CurrentUser.OwnedPlaces;
            ownedPlaceIds = new HashSet<int>(OwnedPlaces.Select(place => place.Id));
            InitializeComponent();
            OrdersCollectionView.ItemsSource = _service.OrderedDeals.Where(order =>
                ownedPlaceIds.Contains(order.PurchasedDeal.FoodPlaceId));

        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrdersCollectionView.ItemsSource = null;
            OrdersCollectionView.ItemsSource = _service.OrderedDeals.Where(order =>
                ownedPlaceIds.Contains(order.PurchasedDeal.FoodPlaceId));
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
                if (order.Status.Equals("Preparing"))
                {
                    PopupNavigation.Instance.PushAsync(new OrderStatusPlacePopup(order));
                }
            });
        }
    }
}
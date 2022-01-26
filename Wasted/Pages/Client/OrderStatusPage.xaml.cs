using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage : ContentPage
    {
        private IEnumerable<OrderDeal> OrderedDeals { get; set; }
        private readonly UserDetailsService _detailsService;

        public OrderStatusPage()
        {
            _detailsService = DependencyService.Get<UserDetailsService>();
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderedDeals = DataProvider.GetClientOrders(_detailsService.UserId)
                .Where(order => order.Status != OrderStatus.InCart);

            OrderedDealsCollectionView.ItemsSource = null; //TODO: inspect why this helps
            OrderedDealsCollectionView.ItemsSource = OrderedDeals;
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
                OrderDeal order = e.CurrentSelection.FirstOrDefault() as OrderDeal;
                if (order.Status.Equals(OrderStatus.ReadyToPickUp))
                {
                    PopupNavigation.Instance.PushAsync(new OrderStatusClientPopup(order));
                }
            });
        }
    }
}
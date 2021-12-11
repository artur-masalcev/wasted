using System;
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
        private readonly DataService _service;

        public OrderStatusPage()
        {
            _service = DependencyService.Get<DataService>();
            InitializeComponent();
        }

        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderedDealsCollectionView.ItemsSource = null;
            OrderedDealsCollectionView.ItemsSource = _service.OrderedDeals;
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
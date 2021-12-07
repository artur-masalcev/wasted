using System;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Pages.Client;
using Wasted.Utils;
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

        private void OrderedDealsCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
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
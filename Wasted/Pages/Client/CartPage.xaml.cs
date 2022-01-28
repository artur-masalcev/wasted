using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private readonly UserDetailsService _detailsService;

        private List<OrderDeal> CurrentCartDeals { get; set; }


        public CartPage()
        {
            InitializeComponent();
            _detailsService = DependencyService.Get<UserDetailsService>();
            On<iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Sets CartDealsCollectionView with CartDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CurrentCartDeals = OrdersProvider.GetClientOrdersByIdAndStatus(_detailsService.UserId, OrderStatus.InCart);
            CartDealsCollectionView.ItemsSource = null;
            CartDealsCollectionView.ItemsSource = CurrentCartDeals;
            Total.Text = "Total " + CurrentCartDeals.Sum(deal => deal.Cost) + " eur.";
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        private void ClickedPurchase(object sender, EventArgs e)
        {
            if (CurrentCartDeals.Any())
            {
                CurrentCartDeals.ForEach(cartDeal => cartDeal.Status = OrderStatus.WaitingForAcceptance);
                OrdersProvider.UpdateOrders(CurrentCartDeals);
                OnAppearing();
                UserDialogs.Instance.Toast("Purchased successfully", new TimeSpan(3));
            }
            else
            {
                UserDialogs.Instance.Toast("Cart is empty", new TimeSpan(3));
            }
        }
    }
}
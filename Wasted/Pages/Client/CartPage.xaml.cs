using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private readonly CurrentUserService _service;
        private List<OrderDeal> AllCartDeals { get; set; }

        private List<OrderDeal> CurrentCartDeals =>
            AllCartDeals.Where(order => order.Status == OrderStatus.InCart)
                .ToList();
        

        public CartPage()
        {
            InitializeComponent();
            _service = DependencyService.Get<CurrentUserService>();
            AllCartDeals = DataProvider.GetClientOrders(_service.CurrentUser.Id);
        }

        /// <summary>
        /// Sets CartDealsCollectionView with CartDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
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
                List<OrderDeal> dealsToUpdate = new List<OrderDeal>(CurrentCartDeals);
                foreach (OrderDeal currentCartDeal in dealsToUpdate)
                {
                    currentCartDeal.Status = OrderStatus.Preparing;
                }
                DataProvider.UpdateOrdersStatus(dealsToUpdate);
                OnAppearing();
                UserDialogs.Instance.Toast("Purchased successfully", new TimeSpan(3));
                Navigation.PushAsync(new OrderStatusPage());
            }
            else
            {
                UserDialogs.Instance.Toast("Cart is empty", new TimeSpan(3));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private DataService Service;
        public OrderStatusPage(DataService service)
        {
            InitializeComponent();

            Service = service;

            foreach (CartDeal cartDeal in service.CartDeals)
            {
                service.OrderedDeals.Add(new OrderedDeal(cartDeal, "preparing"));
            }

            service.CartDeals = new List<CartDeal>();
            
            OrderedDealsCollectionView.ItemsSource = service.OrderedDeals;
        }

        public OrderStatusPage()
        {
            InitializeComponent();
            
            Service = DependencyService.Get<DataService>();
            
            OrderedDealsCollectionView.ItemsSource = Service.OrderedDeals;
        }
        
        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrderedDealsCollectionView.ItemsSource = Service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }
    }
}
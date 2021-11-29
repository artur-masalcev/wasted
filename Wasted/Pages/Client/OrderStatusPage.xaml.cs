using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage : ContentPage
    {
        public DataService Service { get; set; }
        public OrderStatusPage()
        {
            Service = DependencyService.Get<DataService>();
        }
        
        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            InitializeComponent();
            base.OnAppearing();
            foreach (CartDeal cartDeal in Service.CartDeals)
            {
                Service.OrderedDeals.Add(new OrderedDeal(cartDeal, "preparing"));
            }
            Service.CartDeals = new List<CartDeal>();
            OrderedDealsCollectionView.ItemsSource = Service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }
    }
}
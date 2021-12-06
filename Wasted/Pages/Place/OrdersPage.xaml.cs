using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private DataService service;
        public UserPlace CurrentUser { get; set; }
        private IEnumerable<FoodPlace> ownedPlaces;
        public IEnumerable<FoodPlace> OwnedPlaces
        {
            get { return ownedPlaces; }
            set
            {
                ownedPlaces = value;
                OnPropertyChanged();
            }
        }
        public OrdersPage()
        {
            service = DependencyService.Get<DataService>();
            CurrentUser = (UserPlace)service.CurrentUser;
            OwnedPlaces = CurrentUser.OwnedPlaceIDs.Select(id => service.AllFoodPlaces[id - 1]);
            InitializeComponent();

            OrdersCollectionView.ItemsSource = service.OrderedDeals.Where(order =>
            {
                foreach (FoodPlace place in OwnedPlaces)
                {
                    if (place.Title.Equals(order.Title))
                    {
                        return true;
                    }
                }

                return false;
            });
        }
        
        /// <summary>
        /// Sets OrderedDealsCollectionView with OrderedDeals
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrdersCollectionView.ItemsSource = service.OrderedDeals;
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }

        private void OrdersCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                OrderedDeal order = e.CurrentSelection.FirstOrDefault() as OrderedDeal;
                if (order.Status.Equals("Preparing"))
                {
                    order.Status = "Ready to pick up";
                }
            });
        }
    }
}
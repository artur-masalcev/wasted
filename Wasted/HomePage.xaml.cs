using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public List<FoodPlace> AllFoodPlaces { get; set; }
        public List<Deal> AllDeals { get; set; }

        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Puts restaurants from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            nearbyRestaurantsCollectionView.ItemsSource = AllFoodPlaces;

            AllDeals = new List<Deal>(DummyDataProvider.GetDealsList());
            specialOffersCollectionView.ItemsSource = AllDeals;

            popularRestaurantsCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void RestaurantsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRestaurantsSelectionData(e.CurrentSelection);
        }

        void UpdateRestaurantsSelectionData(IEnumerable<object> currentSelectedContact)
        {
            FoodPlace selectedPlace = currentSelectedContact.FirstOrDefault() as FoodPlace;
            //TODO: pass selectedPlace to Restaurant activity.
        }

        /// <summary>
        /// Function is called when user selects deal from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDealsSelectionData(e.CurrentSelection);
        }

        void UpdateDealsSelectionData(IEnumerable<object> currentSelectedContact)
        {
            FoodPlace selectedPlace = currentSelectedContact.FirstOrDefault() as FoodPlace;
            //TODO: pass selectedPlace to Deal activity.
        }
    }
}
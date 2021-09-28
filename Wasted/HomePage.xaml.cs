using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;

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

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts restaurants from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            AllDeals = new List<Deal>(DummyDataProvider.GetDealsList());
            HashMaps.AddRestaurantsToDeals(AllFoodPlaces);

            nearbyRestaurantsCollectionView.ItemsSource = AllFoodPlaces;
            specialOffersCollectionView.ItemsSource = AllDeals;

            popularRestaurantsCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void RestaurantsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
        }

        /// <summary>
        /// Function is called when user selects deal from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
            //TODO: pass selectedPlace to Deal activity.
        }
    }
}
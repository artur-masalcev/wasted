using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public Location UserLocation { get; set; }

        const int MaxRadiusInKilometers = 50;
        const int MaxNearbyPlacesCount = 10;
        const int MaxSpecialOffersCount = 10;
        const int MaxPopularPlacesCount = 10;

        public HomePage()
        {
            InitializeComponent();
            UserLocation = GetLocation().Result;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Gets user location.
        /// </summary>
        public async Task<Location> GetLocation()
        {
            //Uncomment this for the first time, then comment it later. GPS should be available
            //await Geolocation.GetLocationAsync();
            return await Geolocation.GetLastKnownLocationAsync();
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            nearbyFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetNearbyPlaces(App.AllFoodPlaces, UserLocation, MaxNearbyPlacesCount, MaxRadiusInKilometers);

            specialOffersCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetSpecialOffers(App.AllDeals, MaxSpecialOffersCount);

            popularFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetPopularFoodPlaces(App.AllFoodPlaces, MaxPopularPlacesCount);
        }

        /// <summary>
        /// Function is called when user selects food place from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void FoodPlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((CollectionView)sender).SelectedItem == null)
                return;

            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
            ((CollectionView)sender).SelectedItem = null; //Prevents borders from appearing
        }

        /// <summary>
        /// Function is called when user selects deal from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (specialOffersCollectionView.SelectedItem == null)
                return;

            Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
            Navigation.PushAsync(new ItemsPage(selectedDeal));
            specialOffersCollectionView.SelectedItem = null; //Prevents borders from appearing
        }
    }
}
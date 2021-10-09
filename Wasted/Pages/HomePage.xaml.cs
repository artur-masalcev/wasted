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
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            AllDeals = new List<Deal>(DummyDataProvider.GetDealsList());
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces);

            nearbyFoodPlacesCollectionView.ItemsSource = AllFoodPlaces;
            specialOffersCollectionView.ItemsSource = GetSpecialOffers(AllDeals, 10);
            popularFoodPlacesCollectionView.ItemsSource = GetPopularFoodPlaces(AllFoodPlaces, 10);
        }

        /// <summary>
        /// Sorts deals by the percentage of change in cost. Takes the first 'offerCount' of them.
        /// </summary>
        public IEnumerable<Deal> GetSpecialOffers(List<Deal> allDeals, int offerCount)
        {
            return allDeals.OrderBy(deal => deal.CurrentCost / deal.PreviousCost).Take(offerCount);
        }

        /// <summary>
        /// Sorts food places by the number of ratings.
        /// </summary>
        public IEnumerable<FoodPlace> GetPopularFoodPlaces(List<FoodPlace> allFoodPlaces, int offerCount)
        {
            return allFoodPlaces.OrderBy(place => -place.RatingCount).Take(offerCount);
        }

        /// <summary>
        /// Function is called when user selects food place from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void FoodPlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
        }

        /// <summary>
        /// Function is called when user selects deal from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
            Navigation.PushAsync(new ItemsPage(selectedDeal));
        }
    }
}
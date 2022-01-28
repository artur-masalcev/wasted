using System;
using System.Linq;
using Wasted.Pages.Client.DealPage;
using Wasted.Utils;
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
    public partial class HomePage : ContentPage
    {
        private readonly UserDetailsService _detailsService;

        public HomePage()
        {
            _detailsService = DependencyService.Get<UserDetailsService>();
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            NearbyFoodPlacesCollectionView.ItemsSource =
                FoodPlacesProvider.GetClosestFoodPlaces(_detailsService.UserLocation, nearbyPlacesCount: 10, maxRadiusInKilometers: 50);

            SpecialOffersCollectionView.ItemsSource =
                DealsProvider.GetBestOffers(specialOffersCount: 10);

            PopularFoodPlacesCollectionView.ItemsSource =
                FoodPlacesProvider.GetTopRatedFoodPlaces(popularPlacesCount: 10);
        }

        /// <summary>
        /// Function is called when user selects food place from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void FoodPlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
            });
        }

        /// <summary>
        /// Function is called when user selects deal from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
                Navigation.PushAsync(new ItemsPage(selectedDeal));
            });
        }

        /// <summary>
        /// Refreshes the page on scroll down.
        /// </summary>
        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            RefreshView.IsRefreshing = false;
        }
    }
}
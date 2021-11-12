using System.Linq;
using Wasted.DummyAPI;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        private DataService service;

        public HomePage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            nearbyFoodPlacesCollectionView.ItemsSource =
                DataOrganizer.SortPlacesByUserLocation(service.AllFoodPlaces, service.UserLocation, nearbyPlacesCount:10, maxRadiusInKilometers:50);

            var specialOffers
                = DataOrganizer.FilterDeals(
                    DataOrganizer.SortOffersByPriceChange(service.AllDeals, specialOffersCount: 10),
                    DefaultFilters.DealInStock
                ); 
            specialOffersCollectionView.ItemsSource = specialOffers;

            popularFoodPlacesCollectionView.ItemsSource =
                DataOrganizer.SortPlacesByRatingDescending(service.AllFoodPlaces, popularPlacesCount:10);
        }

        /// <summary>
        /// Function is called when user selects food place from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void FoodPlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
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
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
                Navigation.PushAsync(new ItemsPage(selectedDeal));
            });
        }
    }
}
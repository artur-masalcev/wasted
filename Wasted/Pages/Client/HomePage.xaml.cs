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
using System.Threading;
using Wasted.Utils;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        private DataService service;
        public Location UserLocation { get; set; }

        const int MaxRadiusInKilometers = 50;
        const int MaxNearbyPlacesCount = 10;
        const int MaxSpecialOffersCount = 10;
        const int MaxPopularPlacesCount = 10;

        public HomePage()
        {
            service = DependencyService.Get<DataService>();
            GetLocation().Wait();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Gets user location. Not fully working
        /// </summary>
        public async Task GetLocation()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                Task.Run(async () => UserLocation = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)))).Wait();
            }
            else
            {
                var statusAsync = Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                new Thread(() => statusAsync.GetAwaiter().GetResult()).Start();
                Thread.Sleep(5);
                await GetLocation();
            }            
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            nearbyFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetNearbyPlaces(service.AllFoodPlaces, UserLocation, MaxNearbyPlacesCount, MaxRadiusInKilometers);

            specialOffersCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetSpecialOffers(service.AllDeals, MaxSpecialOffersCount);

            popularFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetPopularFoodPlaces(service.AllFoodPlaces, MaxPopularPlacesCount);
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
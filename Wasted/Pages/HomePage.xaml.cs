using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using System.Threading;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public Location UserLocation { get; set; }

        public HomePage()
        {
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

            nearbyFoodPlacesCollectionView.ItemsSource = GetNearbyPlaces(App.AllFoodPlaces, 50);
            specialOffersCollectionView.ItemsSource = GetSpecialOffers(App.AllDeals, 10);
            popularFoodPlacesCollectionView.ItemsSource = GetPopularFoodPlaces(App.AllFoodPlaces, 10);
        }


        /// <summary>
        /// Sorts food places by the location to the user.
        /// </summary>
        public IEnumerable<FoodPlace> GetNearbyPlaces(List<FoodPlace> allFoodPlaces, int maxKilometers)
        {
            Func<Location, FoodPlace, double> GetDistance = (location, place) => Location.CalculateDistance(location, place.PlaceLocation, DistanceUnits.Kilometers);

            return from place in allFoodPlaces
                   orderby GetDistance(UserLocation, place)
                   where GetDistance(UserLocation, place) <= maxKilometers
                   select place;
        }

        /// <summary>
        /// Sorts deals by the percentage of change in cost. Takes the first 'offerCount' of them.
        /// </summary>
        public IEnumerable<Deal> GetSpecialOffers(List<Deal> allDeals, int offerCount)
        {
            return (
                   from deal in allDeals
                   orderby deal.DealCosts.Change()
                   select deal
                   )
                   .Take(offerCount);
        }

        /// <summary>
        /// Sorts food places by the number of ratings.
        /// </summary>
        public IEnumerable<FoodPlace> GetPopularFoodPlaces(List<FoodPlace> allFoodPlaces, int offerCount)
        {
            return (
                   from place in allFoodPlaces
                   orderby -place.RatingCount
                   select place
                   )
                   .Take(offerCount);
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
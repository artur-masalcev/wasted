using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

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
            AddRestaurantsToDeals();

            nearbyRestaurantsCollectionView.ItemsSource = AllFoodPlaces;
            specialOffersCollectionView.ItemsSource = AllDeals;

            popularRestaurantsCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Adds restaurants from which deals came from by using hashmaps.
        /// </summary>
        public void AddRestaurantsToDeals()
        {
            foreach (FoodPlace foodPlace in AllFoodPlaces)
            {
                foreach (int dealId in foodPlace.DealsIDs)
                {
                    Deal deal = HashMaps.DealsHashMap[dealId];
                    deal.FoodPlaces.Add(foodPlace);
                }
            }
        }

        /// <summary>
        /// Function is called when user selects food place from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void RestaurantsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRestaurantsSelectionData(e.CurrentSelection);
        }

        void UpdateRestaurantsSelectionData(IEnumerable<object> currentSelectedFoodPlace)
        {
            FoodPlace selectedPlace = currentSelectedFoodPlace.FirstOrDefault() as FoodPlace;
            //TODO: pass selectedPlace to Restaurant activity.
        }

        /// <summary>
        /// Function is called when user selects deal from nearby restaurants collectionView. Called from xaml file.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDealsSelectionData(e.CurrentSelection);
        }

        void UpdateDealsSelectionData(IEnumerable<object> currentSelectedDeal)
        {
            Deal selectedDeal = currentSelectedDeal.FirstOrDefault() as Deal;
            //TODO: pass selectedPlace to Deal activity.
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using System.Collections.ObjectModel;
using Wasted.Dummy_API;
using System.Text.RegularExpressions;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public List<FoodPlace> AllFoodPlaces { get; set; }
        public List<FoodPlace> AvailablePlaces { get; set; }

        public string[] PlaceTypeNames = Enum.GetNames(typeof(PlaceType));
        public string CurrentPlaceType { get; set; }

        public SearchPage()
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
            DummyDataProvider.GetDealsList(); //Adds deals to hashmap.
            HashMaps.AddFoodPlacesToDeals(AllFoodPlaces);

            restaurantTypeCollectionView.ItemsSource = getRestaurantTypeCollectionViewItemsSource();
            sectionTitle.Text = "All places";
        }

        private Pair<string, int>[] getRestaurantTypeCollectionViewItemsSource()
        {
            
            Pair<string, int>[] source = new Pair<string, int>[PlaceTypeNames.Length];

            for (int i = 0; i < PlaceTypeNames.Length; ++i)
            {
                source[i] = new Pair<string, int>(PlaceTypeNames[i], i);
            }
            return source;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby places collectionView. Called from xaml file.
        /// </summary>
        private void PlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
        }

        private void ShowFoodPlaces(bool show = true)
        {
            restaurantLayout.IsVisible = show;
            restaurantTypeCollectionView.IsVisible = !show;
        }

        /// <summary>
        /// Function is called when user selects place type collectionView. Called from xaml file.
        /// </summary>
        private void TypesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (restaurantTypeCollectionView.SelectedItem == null) //Called from allPlaceButton.
                return;

            ShowFoodPlaces();
            int index = ((Pair<string, int>)e.CurrentSelection.FirstOrDefault()).Second;
            AvailablePlaces = HashMaps.FoodPlaceTypeHashMap[index];
            allPlacesCollectionView.ItemsSource = AvailablePlaces;

            sectionTitle.Text = PlaceTypeNames[index] + "s";
            allPlaceButton.IsVisible = true;
            allPlaceButton.Text = "x " + PlaceTypeNames[index] + "s";

        }

        private void AllPlaceButtonClicked(object sender, EventArgs e)
        {
            ShowFoodPlaces(false);
            allPlaceButton.IsVisible = false;
            allPlacesCollectionView.ItemsSource = AllFoodPlaces;
            sectionTitle.Text = "All places";
            restaurantTypeCollectionView.SelectedItem = null;
        }

        /// <summary>
        /// Function that changes the drop down elements when something is written into the search box.
        /// </summary>
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            const string restaurantNameRegex = @"^[a-zA-Z0-9ąčęėįšųūĄČĘĖĮŠŲŪ' ]*$";

            try
            {
                Func<FoodPlace, bool> maskFunction = i => i.Title.ToLower().Contains(e.NewTextValue.ToLower());
                var filteredPlaces = AvailablePlaces.Where(maskFunction);

                bool isValid = Regex.IsMatch(e.NewTextValue, restaurantNameRegex);
                ((Xamarin.Forms.Entry)sender).TextColor = isValid ? Color.Default : Color.Red;

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    ShowFoodPlaces(false);
                    allPlacesCollectionView.ItemsSource = AvailablePlaces;
                }   
                else
                {
                    ShowFoodPlaces();
                    allPlacesCollectionView.ItemsSource = filteredPlaces;
                }     
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null reference exception");
            }
        }
    }
}
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
using Wasted.Utils;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private DataService service;
        public List<FoodPlace> AvailablePlaces { get; set; }

        public string[] PlaceTypeNames { get; set; } = HashMaps.PlaceTypeDictionary.Keys.ToArray();

        private string currentPlaceType;
        public string CurrentPlaceType
        {
            get { return currentPlaceType; }
            set
            { 
                currentPlaceType = value;
                OnPropertyChanged();
            }
        }

        private string sectionTitleText = "All places";
        public string SectionTitleText
        {
            get { return sectionTitleText; }
            set
            {
                sectionTitleText = value;
                OnPropertyChanged();
            }
        }

        private string allPlaceButtonText;
        public string AllPlaceButtonText
        {
            get { return allPlaceButtonText; }
            set
            {
                allPlaceButtonText = value;
                OnPropertyChanged();
            }
        }

        public SearchPage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AvailablePlaces = service.AllFoodPlaces;
            restaurantLayout.BindingContext = this;
            restaurantTypeCollectionView.ItemsSource = PlaceTypeNames;
        
        }

        /// <summary>
        /// Function is called when user selects food place from nearby places collectionView. Called from xaml file.
        /// </summary>
        private void PlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allPlacesCollectionView.SelectedItem == null)
                return;

            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
            allPlacesCollectionView.SelectedItem = null; //Prevents borders from appearing
        }

        /// <summary>
        /// Swaps place type window to places window and otherwise
        /// </summary>
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

            string type = (string)e.CurrentSelection.FirstOrDefault();

            AvailablePlaces = HashMaps.PlaceTypeDictionary[type];
            allPlacesCollectionView.ItemsSource = AvailablePlaces;

            SectionTitleText = type + "s";

            allPlaceButton.IsVisible = true; //Return to all places from current place type
            AllPlaceButtonText = SectionTitleText;

        }

        /// <summary>
        /// Clicked on x. Resets restaurants to show all of them.
        /// </summary>
        private void AllPlaceButtonClicked(object sender, EventArgs e)
        {
            ShowFoodPlaces(false);
            allPlaceButton.IsVisible = false;
            AvailablePlaces = service.AllFoodPlaces;
            allPlacesCollectionView.ItemsSource = AvailablePlaces;

            SectionTitleText = "All places";
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
                searchBar.TextColor = isValid ? Color.Default : Color.Red;

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
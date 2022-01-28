using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public partial class SearchPage : ContentPage
    {
        public List<FoodPlace> AvailablePlaces { get; set; }

        public List<FoodPlace> AllPlaces { get; set; }
        public List<FoodPlaceType> FoodPlaceTypes { get; set; }
        public string[] PlaceTypeNames { get; set; }

        private string _currentPlaceType;

        public string CurrentPlaceType
        {
            get => _currentPlaceType;
            set
            {
                _currentPlaceType = value;
                OnPropertyChanged();
            }
        }

        private string _sectionTitleText = "All places";

        public string SectionTitleText
        {
            get => _sectionTitleText;
            set
            {
                _sectionTitleText = value;
                OnPropertyChanged();
            }
        }

        private string _allPlaceButtonText;

        public string AllPlaceButtonText
        {
            get => _allPlaceButtonText;
            set
            {
                _allPlaceButtonText = value;
                OnPropertyChanged();
            }
        }

        public SearchPage()
        {
            FoodPlaceTypes = FoodPlacesProvider.GetFoodPlaceTypes();
            PlaceTypeNames = FoodPlaceTypes.Select(type => type.Type).ToArray();
            AllPlaces = FoodPlaceTypes
                .SelectMany(type => type.FoodPlaces)
                .ToList();

            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AvailablePlaces = AllPlaces;
            RestaurantLayout.BindingContext = this;
            RestaurantTypeCollectionView.ItemsSource = PlaceTypeNames;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby places collectionView. Called from xaml file.
        /// </summary>
        private void PlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
            });
        }

        /// <summary>
        /// Swaps place type window to places window and otherwise
        /// </summary>
        private void ShowFoodPlaces(bool show)
        {
            RestaurantLayout.IsVisible = show;
            RestaurantTypeCollectionView.IsVisible = !show;
        }

        /// <summary>
        /// Function is called when user selects place type collectionView. Called from xaml file.
        /// </summary>
        private void TypesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RestaurantTypeCollectionView.SelectedItem == null) //Called from allPlaceButton.
                return;

            ShowFoodPlaces(true);

            string type = (string) e.CurrentSelection.FirstOrDefault();
            AvailablePlaces = FoodPlaceTypes.Find(t => t.Type == type).FoodPlaces;
            AllPlacesCollectionView.ItemsSource = AvailablePlaces;

            SectionTitleText = type + "s";
            AllPlaceButton.IsVisible = true; //Return to all places from current place type
            AllPlaceButtonText = SectionTitleText;
        }

        /// <summary>
        /// Clicked on x. Resets restaurants to show all of them.
        /// </summary>
        private void AllPlaceButtonClicked(object sender, EventArgs e)
        {
            ShowFoodPlaces(false);
            AllPlaceButton.IsVisible = false;
            AvailablePlaces = AllPlaces;
            AllPlacesCollectionView.ItemsSource = AvailablePlaces;

            SectionTitleText = "All places";
            RestaurantTypeCollectionView.SelectedItem = null;
        }

        /// <summary>
        /// Function that changes the drop down elements when something is written into the search box.
        /// </summary>
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            const string restaurantNameRegex = @"^[a-zA-Z0-9ąčęėįšųūĄČĘĖĮŠŲŪ' ]*$";

            try
            {
                bool MaskFunction(FoodPlace i) => i.Title.ToLower().Contains(e.NewTextValue.ToLower());
                var filteredPlaces = AvailablePlaces.Where(MaskFunction);

                bool isValid = Regex.IsMatch(e.NewTextValue, restaurantNameRegex);
                SearchBar.TextColor = isValid ? Color.Default : Color.Red;

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    ShowFoodPlaces(false);
                    AllPlacesCollectionView.ItemsSource = AvailablePlaces;
                }
                else
                {
                    ShowFoodPlaces(true);
                    AllPlacesCollectionView.ItemsSource = filteredPlaces;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null reference exception");
            }
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
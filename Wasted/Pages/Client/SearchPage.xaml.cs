﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public List<FoodPlace> AvailablePlaces { get; set; }

        public List<FoodPlace> AllPlaces { get; set; }
        public List<FoodPlaceType> FoodPlaceTypes { get; set; }
        public string[] PlaceTypeNames { get; set; }

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
            DataService service = DependencyService.Get<DataService>();
            FoodPlaceTypes = service.GetFoodPlaceTypes;
            PlaceTypeNames = FoodPlaceTypes.Select(type => type.Type).ToArray();
            AllPlaces = FoodPlaceTypes
                .Aggregate(new List<FoodPlace>(), (acc, next) => acc.Concat(next.FoodPlaces).ToList()) 
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
            restaurantLayout.BindingContext = this;
            restaurantTypeCollectionView.ItemsSource = PlaceTypeNames;
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

            ShowFoodPlaces(true);

            string type = (string)e.CurrentSelection.FirstOrDefault();
            AvailablePlaces = FoodPlaceTypes.Find(t => t.Type == type).FoodPlaces;
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
            AvailablePlaces = AllPlaces;
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
                bool MaskFunction(FoodPlace i) => i.Title.ToLower().Contains(e.NewTextValue.ToLower());
                var filteredPlaces = AvailablePlaces.Where(MaskFunction);

                bool isValid = Regex.IsMatch(e.NewTextValue, restaurantNameRegex);
                searchBar.TextColor = isValid ? Color.Default : Color.Red;

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    ShowFoodPlaces(false);
                    allPlacesCollectionView.ItemsSource = AvailablePlaces;
                }
                else
                {
                    ShowFoodPlaces(true);
                    allPlacesCollectionView.ItemsSource = filteredPlaces;
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
            refreshView.IsRefreshing = false;
        }
    }
}
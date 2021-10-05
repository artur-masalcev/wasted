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

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public List<FoodPlace> AllFoodPlaces { get; set; }

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

            allPlacesCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby places collectionView. Called from xaml file.
        /// </summary>
        private void PlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
            Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
        }

        /// <summary>
        /// Function that changes the drop down elements when something is written into the search box.
        /// </summary>
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            const string passwordRegex = @"^[a-zA-Z0-9ąčęėįšųūĄČĘĖĮŠŲŪ']*$";

            try
            {
                Func<FoodPlace, bool> maskFunction = i => i.Title.ToLower().Contains(e.NewTextValue.ToLower());
                var filteredPlaces = AllFoodPlaces.Where(maskFunction);

                bool isValid = Regex.IsMatch(e.NewTextValue, passwordRegex);
                ((Xamarin.Forms.Entry)sender).TextColor = isValid ? Color.Default : Color.Red;

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    allPlacesCollectionView.ItemsSource = AllFoodPlaces;
                }   
                else
                {
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
using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using System.Collections.ObjectModel;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public List<FoodPlace> AllFoodPlaces { get; set; }
        ObservableCollection<string> FoodPlacesNames = new ObservableCollection<string>();

        public SearchPage()
        {
            InitializeComponent();
            SetFoodPlaceObservableList();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts restaurants from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            allPlacesCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Function is called when user selects food place from nearby places collectionView. Called from xaml file.
        /// </summary>
        void PlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlacesSelectionData(e.CurrentSelection);
        }

        void UpdatePlacesSelectionData(IEnumerable<object> currentSelectedFoodPlace)
        {
            FoodPlace selectedPlace = currentSelectedFoodPlace.FirstOrDefault() as FoodPlace;
            //TODO: pass selectedPlace to Place activity.
        }

        // <summary>
        // Function that sets the observable list of food place names.
        // </summary>
        public void SetFoodPlaceObservableList()
        {
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            foreach (FoodPlace OneFoodPlace in AllFoodPlaces)
            {
                FoodPlacesNames.Add(OneFoodPlace.Title);
            }
        }

        /// <summary>
        /// Function that changes the drop down elements when something is written into the search box.
        /// </summary>
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            try
            {
                var dataEmpty = FoodPlacesNames.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue) || dataEmpty.Max().Length == 0)
                {
                    allPlacesCollectionView.ItemsSource = AllFoodPlaces;
                }   
                else
                {                    
                    allPlacesCollectionView.ItemsSource = AllFoodPlaces.Where(i => i.Title.ToLower().Contains(e.NewTextValue.ToLower()));
                }
            }
            catch (NullReferenceException ex)
            {
                // do nothing
            }
        }
    }
}
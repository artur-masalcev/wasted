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

        public void SetFoodPlaceObservableList()
        {
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            foreach (FoodPlace OneFoodPlace in AllFoodPlaces)
            {
                FoodPlacesNames.Add(OneFoodPlace.Title);
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            placesListView.IsVisible = true;
            placesListView.BeginRefresh();

            try
            {
                var dataEmpty = FoodPlacesNames.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    placesListView.IsVisible = false;
                else if (dataEmpty.Max().Length == 0)
                    placesListView.IsVisible = false;
                else
                    placesListView.ItemsSource = FoodPlacesNames.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (exception ex)
            {
                placesListView.IsVisible = false;
            }
            placesListView.EndRefresh();
        }

        private void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  

            String list = e.Item as string;
            searchBar.Text = list;
            placesListView.IsVisible = false;

            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }
    }
}
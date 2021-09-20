using Sharpnado.HorizontalListView.RenderedViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public List<FoodPlace> AllFoodPlaces { get; set; }

        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Puts restaurants from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AllFoodPlaces = new List<FoodPlace>(DummyDataProvider.GetFoodPlacesList());
            foodPlaceCollectionView.ItemsSource = AllFoodPlaces;
        }

        /// <summary>
        /// Function is called when user selects food place from collectionView.
        /// </summary>
        void CollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectionData(e.CurrentSelection);
        }

        void UpdateSelectionData(IEnumerable<object> currentSelectedContact)
        {
            FoodPlace selectedPlace = currentSelectedContact.FirstOrDefault() as FoodPlace;
            //TODO: pass selectedPlace to Restaurant activity.
        }


    }
}
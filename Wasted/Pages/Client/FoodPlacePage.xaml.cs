using System;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPlacesPage : ContentPage
    {
        public FoodPlace SelectedFoodPlace { get; set; }

        public FoodPlacesPage(FoodPlace selectedFoodPlace)
        {
            SelectedFoodPlace = selectedFoodPlace;

            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            InitializeViews();
        }

        /// <summary>
        /// Sets binding for xaml file.
        /// </summary>
        public void InitializeViews()
        {
            contentScrollView.BindingContext = SelectedFoodPlace;
            dealsCollectionView.ItemsSource = DataOrganizer.FilterDeals(SelectedFoodPlace.Deals,
                DefaultFilters.DealInStock);

            const int dealHeight = 220; //Dirty fix to prevent too much scroll area
            dealsCollectionView.HeightRequest = dealHeight * ((SelectedFoodPlace.Deals.Count  + 1) / 2); //Two items in one row
        }

        /// <summary>
        /// Goes to navigation page.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
                Navigation.PushAsync(new ItemsPage(selectedDeal));
            });
        }

        /// <summary>
        /// Shows rating popup.
        /// </summary>
        private void OnRateButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new RatingPopup(SelectedFoodPlace));
        }
    }  
}
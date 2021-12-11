using System;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Pages.Client.DealPage;
using Wasted.Pages.Client.FoodPlaceRating;
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
    public partial class FoodPlacesPage : ContentPage
    {
        public FoodPlace SelectedFoodPlace { get; set; }
        private DataService service;

        public FoodPlacesPage(FoodPlace selectedFoodPlace)
        {
            SelectedFoodPlace = selectedFoodPlace;

            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            InitializeViews();

            service = DependencyService.Get<DataService>();
        }

        /// <summary>
        /// Sets binding for xaml file.
        /// </summary>
        private void InitializeViews()
        {
            contentScrollView.BindingContext = SelectedFoodPlace;
            dealsCollectionView.ItemsSource = DataOrganizer.FilterDeals(SelectedFoodPlace.Deals,
                DefaultFilters.DealInStock);
            
            const int dealHeight = 220;
            dealsCollectionView.HeightRequest = dealHeight * ((SelectedFoodPlace.Deals.Count  + 1) / 2); //Two items in one row
        }

        /// <summary>
        /// Goes to navigation page.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
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

        /// <summary>
        /// Refreshes the page on scroll down.
        /// </summary>
        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            SelectedFoodPlace = service.AllFoodPlaces.Find(place => place.Id == SelectedFoodPlace.Id);
            BindingContext = SelectedFoodPlace;
            InitializeViews();
            refreshView.IsRefreshing = false;
        }
    }  
}
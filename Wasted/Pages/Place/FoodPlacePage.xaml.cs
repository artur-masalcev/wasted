using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Pages.Place.NewDeal;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPlacePage : ContentPage
    {
        private FoodPlace SelectedPlace => DataProvider.GetAllFoodPlaces().First(foodPlace => foodPlace.Id == SelectedPlaceId);
        private int SelectedPlaceId;

        public List<Deal> Deals => SelectedPlace.Deals;

        public FoodPlacePage(int selectedPlaceId)
        {
            InitializeComponent();

            SelectedPlaceId = selectedPlaceId;

            BindingContext = this;

            On<iOS>().SetUseSafeArea(true);
        }

        protected override void OnAppearing()
        {
            PageTitleLabel.Text = SelectedPlace.Title;
            DealsCollectionView.ItemsSource = SelectedPlace.Deals;

            base.OnAppearing();
        }
        
        private void DealsCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deal selectedDeal = e.CurrentSelection.First() as Deal;
            Navigation.PushAsync(new DealEditorPage(selectedDeal));
        }
        
        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
        
        private void AddNewDealClicked(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new NewDealPage(SelectedPlace));
        }

        private void EditPlaceClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlaceEditorPage(SelectedPlace));
        }
    }
}
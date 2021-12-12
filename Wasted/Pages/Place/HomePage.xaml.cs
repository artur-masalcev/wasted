using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Wasted.Pages.Place.NewDeal;
using Wasted.Pages.Place.NewPlace;
using Wasted.Utils;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly CurrentUserService _service;
        private PlaceUser _currentPlaceUser;
        public List<FoodPlace> OwnedPlaces => _currentPlaceUser.OwnedPlaces;

        public HomePage()
        {
            _service = DependencyService.Get<CurrentUserService>();
            InitializeComponent();
            _currentPlaceUser = (PlaceUser) _service.CurrentUser;
            BindingContext = this;
            On<iOS>().SetUseSafeArea(true);
        }

        protected override void OnAppearing()
        {
            PlacesCollectionView.ItemsSource = ((PlaceUser) _service.CurrentUser).OwnedPlaces;
            base.OnAppearing();
        }

        private void PlacesCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                var selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;

                if (selectedPlace != null) Navigation.PushAsync(new FoodPlacePage(selectedPlace.Id));
                else DisplayAlert("Error", "Food place does not exist", "OK");
            });
        }

        private void AddNewPlaceButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPlacePage());
        }
    }
}
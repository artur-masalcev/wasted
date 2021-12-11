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
    public partial class PlaceHomePage : ContentPage
    {
        private readonly DataService _service;
        private readonly PlaceUser _currentPlaceUser;
        public List<FoodPlace> OwnedPlaces => _currentPlaceUser.OwnedPlaces;

        public PlaceHomePage()
        {
            _service = DependencyService.Get<DataService>();
            InitializeComponent();
            _currentPlaceUser = (PlaceUser) _service.CurrentUser;
            BindingContext = this;
            On<iOS>().SetUseSafeArea(true);
        }

        private void PlacesCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new FoodPlacePage(selectedPlace));
            });
        }

        private void AddNewPlaceButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPlacePage());
        }
    }
}
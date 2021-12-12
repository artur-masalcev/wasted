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
        private readonly CurrentUserService _service;
        private readonly PlaceUser _currentPlaceUser;
        public List<FoodPlace> OwnedPlaces => _currentPlaceUser.OwnedPlaces;
        public ICommand DeleteCommand { get; set; }

        public PlaceHomePage()
        {
            _service = DependencyService.Get<CurrentUserService>();
            InitializeComponent();
            DeleteCommand = new Command(DeletePlace);
            _currentPlaceUser = (PlaceUser) _service.CurrentUser;
            BindingContext = this;
            On<iOS>().SetUseSafeArea(true);
        }

        private void YourPlacesCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new NewDealPage(selectedPlace));
            });
        }

        private void AddNewPlaceButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPlacePage());
        }

        private void DeletePlace(object obj)
        {
            FoodPlace selectedPlace = obj as FoodPlace;
            DataProvider.DeleteFoodPlace(selectedPlace);
            _currentPlaceUser.OwnedPlaces = _currentPlaceUser.OwnedPlaces
                .Where(place => place != selectedPlace)
                .ToList();
            OnPropertyChanged("OwnedPlaces");
        }
    }
}
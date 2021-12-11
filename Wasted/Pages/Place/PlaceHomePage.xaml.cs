using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Pages.Place.NewDeal;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceHomePage : ContentPage
    {
        private DataService service;
        private PlaceUser currentPlaceUser;
        public List<FoodPlace> OwnedPlaces => currentPlaceUser.OwnedPlaces;
        public ICommand DeleteCommand { get; set; }
        public PlaceHomePage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            DeleteCommand = new Command(DeletePlace);
            currentPlaceUser = (PlaceUser)service.CurrentUser;
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
            currentPlaceUser.OwnedPlaces = currentPlaceUser.OwnedPlaces
                .Where(place => place != selectedPlace)
                .ToList();
            OnPropertyChanged("OwnedPlaces");
        }
    }
}
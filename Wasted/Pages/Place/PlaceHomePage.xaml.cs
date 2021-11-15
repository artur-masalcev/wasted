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
        public UserPlace CurrentUser { get; set; }
        private IEnumerable<FoodPlace> ownedPlaces;
        public IEnumerable<FoodPlace> OwnedPlaces
        {
            get { return ownedPlaces; }
            set
            {
                ownedPlaces = value;
                OnPropertyChanged();
            }
        }
        public ICommand DeleteCommand { get; set; }
        public PlaceHomePage()
        {
            service = DependencyService.Get<DataService>();
            CurrentUser = (UserPlace)service.CurrentUser;
            BindingContext = this;
            OwnedPlaces = CurrentUser.OwnedPlaceIDs.Select(id =>
                service.AllFoodPlaces.Find(place => place.ID == id)
            );
            InitializeComponent();
            DeleteCommand = new Command(DeletePlace);
            
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
            CurrentUser.OwnedPlaceIDs = CurrentUser.OwnedPlaceIDs.Where(id => id != selectedPlace.ID).ToList();
            OwnedPlaces = OwnedPlaces.Where(place => place != selectedPlace);
        }
    }
}
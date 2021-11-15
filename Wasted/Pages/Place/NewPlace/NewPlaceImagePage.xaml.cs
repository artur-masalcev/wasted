using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place.NewPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPlaceImagePage : ContentPage
    {
        public FoodPlace CurrentPlace { get; set; }
        public NewPlaceImagePage(FoodPlace place)
        {
            CurrentPlace = place;
            InitializeComponent();
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void HeaderButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentPlace, "HeaderURL"));
        }

        private void LogoButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentPlace, "LogoURL"));
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            DataService service = DependencyService.Get<DataService>();
            CurrentPlace.ID = service.AllFoodPlaces.Count == 0 ? 1 : service.AllFoodPlaces.Last().ID + 1;

            List<FoodPlace> currentFoodPlaces = service.AllFoodPlaces;
            currentFoodPlaces.Add(CurrentPlace);
            DataProvider.WriteAllPlaces(currentFoodPlaces);

            ((UserPlace)service.CurrentUser).OwnedPlaceIDs.Add(CurrentPlace.ID);
            DataProvider.WriteAllUserPlaces();
            
            ((UserPlace)service.CurrentUser).PushPage(this);
        }
        
        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
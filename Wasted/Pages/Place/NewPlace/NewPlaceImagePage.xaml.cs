using System;
using Rg.Plugins.Popup.Services;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
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

        private void AddPlaceToPlaces()
        {
            ExceptionChecker.CheckValidParams(CurrentPlace.HeaderURL, CurrentPlace.LogoURL);
            DataService service = DependencyService.Get<DataService>();
            PlaceUser currentPlaceUser = (PlaceUser) service.CurrentUser;
            CurrentPlace.PlaceUserId = currentPlaceUser.Id;
            CurrentPlace.FoodPlaceTypeId = 1; //TODO: let select
            DataProvider.CreateFoodPlace(CurrentPlace);
            service.UpdateUserInfo(); //Fetches the id of a new place
            currentPlaceUser.PushPage(this);
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(
                AddPlaceToPlaces,
                this
            );
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
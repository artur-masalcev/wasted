using System;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
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

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                UserDetailsService detailsService = DependencyService.Get<UserDetailsService>();
                PlaceUser currentPlaceUser = UsersProvider.GetPlaceUser(detailsService);
                CurrentPlace.PlaceUserId = currentPlaceUser.Id;
                CurrentPlace.FoodPlaceTypeId = 1; //TODO: let select
                FoodPlacesProvider.CreateFoodPlace(CurrentPlace);
                currentPlaceUser.InitializePage(this);
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(CurrentPlace.HeaderURL, CurrentPlace.LogoURL);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
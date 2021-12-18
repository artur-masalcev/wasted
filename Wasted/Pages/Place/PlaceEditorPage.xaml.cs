using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceEditorPage : ContentPage
    {
        public FoodPlace SelectedPlace { get; set; }
        public EntryLengthValidator Validator { get; set; }

        private string NewPlaceTitle => TitleEntry.Text;
        private string NewDescription => DescriptionEntry.Text;
        private string NewHeaderURL => SelectedPlace.HeaderURL;
        private string NewLogoURL => SelectedPlace.LogoURL;

        private CurrentUserService _service = DependencyService.Get<CurrentUserService>();

        public PlaceEditorPage(FoodPlace selectedPlace)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            SelectedPlace = selectedPlace;
            Validator = new EntryLengthValidator(maxEntryLength: 50);

            BindingContext = this;
        }


        /// <summary>
        /// Updates all the fields of 'SelectedPlace' accordingly to user input
        /// </summary>
        /// <notice>
        /// This function does not check if the user input is valid, use 'IsDataValid()' for data validation
        /// </notice>
        private void UpdateSelectedPlaceObject()
        {
            SelectedPlace.Title = NewPlaceTitle;
            SelectedPlace.Description = NewDescription;
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }

        /// <summary>
        /// Publishes updated food place data to the server.
        /// Also checks whether the data is valid, if it is not - alert is displayed
        /// </summary>
        private void SaveChangesClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(() => {
                    ExceptionChecker.CheckValidParams(NewPlaceTitle, NewDescription, NewHeaderURL, NewLogoURL);
                    UpdateSelectedPlaceObject();
                    DataProvider.UpdateFoodPlace(SelectedPlace);
                    _service.UpdateUserInfo();
            }, this);
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(() => {
                    ExceptionChecker.CheckValidParams(NewPlaceTitle, NewDescription, NewHeaderURL, NewLogoURL);
                    UpdateSelectedPlaceObject();
                    Navigation.PushAsync(new FoodPlacePreviewPage(SelectedPlace));
            }, this);
        }

        private void DeletePlaceClicked(object sender, EventArgs e)
        {
            DataProvider.DeleteFoodPlace(SelectedPlace);
            _service.UpdateUserInfo();
            _service.CurrentUser.PushPage(this);
        }

        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }

        private void ChooseHeaderImageClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(SelectedPlace, "HeaderURL"));
        }

        private void ChooseLogoImageClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(SelectedPlace, "LogoURL"));
        }
    }
}
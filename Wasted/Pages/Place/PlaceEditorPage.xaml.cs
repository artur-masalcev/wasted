using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
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

        public PlaceEditorPage(FoodPlace selectedPlace)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            
            SelectedPlace = selectedPlace;
            Validator = new EntryLengthValidator(maxEntryLength: 50);

            BindingContext = this;
        }

        /// <summary>
        /// Checks whether all user fields are not empty
        /// </summary>
        /// <returns>'false' if any of fields is empty. 'true' if all fields are not empty</returns>
        private bool IsDataValid()
        { 
            return !StringUtils.AnyNullOrEmpty(NewPlaceTitle, NewDescription, NewHeaderURL, NewLogoURL);
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
            if (!IsDataValid())
            {
                DisplayAlert("", "Please fill all fields", "OK");
                return;
            }
            
            UpdateSelectedPlaceObject();

            DataProvider.UpdateFoodPlace(SelectedPlace);
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                DisplayAlert("", "Please fill all fields", "OK");
                return;
            }
            
            UpdateSelectedPlaceObject();
            
            Navigation.PushAsync(new FoodPlacePreviewPage(SelectedPlace));
        }

        private void DeletePlaceClicked(object sender, EventArgs e)
        {
            DataProvider.DeleteFoodPlace(SelectedPlace);
            DependencyService.Get<DataService>().CurrentUser.PushPage(this);
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
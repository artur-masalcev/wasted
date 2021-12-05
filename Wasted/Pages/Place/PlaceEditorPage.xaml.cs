using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
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
            Validator = new EntryLengthValidator(maxEntryLength: 30);

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

            SelectedPlace.Title = NewPlaceTitle;
            SelectedPlace.Description = NewDescription;

            //IMPLEMENT ME: Post new place state (as 'SelectedPlace') to server
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeletePlaceClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
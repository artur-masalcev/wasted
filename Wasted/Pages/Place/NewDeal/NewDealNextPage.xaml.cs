using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place.NewDeal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDealNextPage : ContentPage
    {
        public EntryLengthValidator Validator { get; set; }
        public Deal CurrentDeal { get; set; }
        public FoodPlace SelectedPlace { get; set; }
        public NewDealNextPage(Deal currentDeal, FoodPlace selectedPlace)
        {
            CurrentDeal = currentDeal;
            SelectedPlace = selectedPlace;
            Validator = new EntryLengthValidator(maxEntryLength: 100);
            BindingContext = Validator;
            InitializeComponent();
        }

        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }

        private void ImageButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentDeal, "ImageURL"));
        }

        private void FillDealValues(DataService service)
        {
            CurrentDeal.ID = service.AllDeals.Count + 1;
            CurrentDeal.Due = DueDatePicker.Date.ToString("yyyy-MM-dd");
            CurrentDeal.Description = DescriptionEntry.Text;
            CurrentDeal.FoodPlaces.Add(SelectedPlace);

            List<Deal> currentDeals = service.AllDeals;
            currentDeals.Add(CurrentDeal);
            DataProvider.WriteAllDeals(currentDeals);
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            DataService service = DependencyService.Get<DataService>();
            FillDealValues(service);
            
            SelectedPlace.Deals.Add(CurrentDeal);
            SelectedPlace.DealsIDs.Add(CurrentDeal.ID);
            DataProvider.WriteAllPlaces();

            ((UserPlace)service.CurrentUser).PushPage(this);
        }
    }
}
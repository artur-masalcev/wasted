﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
            
            On<iOS>().SetUseSafeArea(true);
        }

        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }

        private void ImageButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentDeal, "ImageURL"));
        }

        private void FillDealValues(DataService service, string dealExpirationDate, string dealDescription)
        {
            ExceptionChecker.CheckValidParams(dealDescription, CurrentDeal.ImageURL);
            CurrentDeal.Due = dealExpirationDate;
            CurrentDeal.Description = dealDescription;
            DataProvider.CreateDeal(CurrentDeal);
            service.CurrentUser.PushPage(this);
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            DataService service = DependencyService.Get<DataService>();
            string dealExpirationDate = DueDatePicker.Date.ToString("yyyy-MM-dd");
            ExceptionHandler.WrapFunctionCall(
                    () => FillDealValues(service, dealExpirationDate, DescriptionEntry.Text),
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
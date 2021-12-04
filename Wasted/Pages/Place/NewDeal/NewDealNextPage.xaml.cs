﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Rg.Plugins.Popup.Services;
// using Wasted.Dummy_API.Business_Objects;
// using Wasted.DummyAPI.BusinessObjects;
// using Wasted.DummyDataAPI;
// using Wasted.Utils;
// using Wasted.Utils.Exceptions;
// using Xamarin.Forms;
// using Xamarin.Forms.PlatformConfiguration;
// using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
// using Xamarin.Forms.Xaml;
//
// namespace Wasted.Pages.Place.NewDeal
// {
//     [XamlCompilation(XamlCompilationOptions.Compile)]
//     public partial class NewDealNextPage : ContentPage
//     {
//         public EntryLengthValidator Validator { get; set; }
//         public Deal CurrentDeal { get; set; }
//         public FoodPlace SelectedPlace { get; set; }
//         public NewDealNextPage(Deal currentDeal, FoodPlace selectedPlace)
//         {
//             CurrentDeal = currentDeal;
//             SelectedPlace = selectedPlace;
//             Validator = new EntryLengthValidator(maxEntryLength: 100);
//             BindingContext = Validator;
//             InitializeComponent();
//             
//             On<iOS>().SetUseSafeArea(true);
//         }
//
//         private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
//         {
//             Validator.EntryTextChanged(sender, e);
//         }
//
//         private void ImageButtonClicked(object sender, EventArgs e)
//         {
//             PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentDeal, "ImageURL"));
//         }
//
//         private void FillDealValues(DataService service, string dealExpirationDate, string dealDescription)
//         {
//             ExceptionChecker.CheckValidParams(dealDescription, CurrentDeal.ImageURL);
//             CurrentDeal.ID = service.AllDeals.Count == 0 ? 1 : service.AllDeals.Last().ID + 1;
//             CurrentDeal.Due = dealExpirationDate;
//             CurrentDeal.Description = dealDescription;
//             CurrentDeal.FoodPlaces.Add(SelectedPlace);
//
//             List<Deal> currentDeals = service.AllDeals;
//             currentDeals.Add(CurrentDeal);
//             DataProvider.WriteAllDeals(currentDeals);
//         }
//
//         private void AddDealToDeals(DataService service, string dealExpirationDate, string dealDescription)
//         {
//             FillDealValues(service, dealExpirationDate, dealDescription);
//             // SelectedPlace.Deals.Add(CurrentDeal);
//             // SelectedPlace.DealsIDs.Add(CurrentDeal.ID);
//             DataProvider.WriteAllPlaces();
//
//             ((UserPlace)service.CurrentUser).PushPage(this);
//         }
//
//         private void DoneButtonClicked(object sender, EventArgs e)
//         {
//             DataService service = DependencyService.Get<DataService>();
//             string dealExpirationDate = DueDatePicker.Date.ToString("yyyy-MM-dd");
//             ExceptionHandler.WrapFunctionCall(
//                     () => AddDealToDeals(service, dealExpirationDate, DescriptionEntry.Text),
//                     this
//                 );
//         }
//         
//         private void BackClicked(object sender, EventArgs e)
//         {
//             Navigation.PopAsync(true);
//             base.OnBackButtonPressed();
//         }
//     }
// }
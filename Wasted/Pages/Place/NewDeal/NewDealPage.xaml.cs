using System;
using Wasted.Utils.Exceptions;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;

namespace Wasted.Pages.Place.NewDeal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDealPage : ContentPage
    {
        public FoodPlace SelectedPlace { get; set; }

        public NewDealPage(FoodPlace selectedPlace)
        {
            SelectedPlace = selectedPlace;
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
        }

        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) &&
                (!double.TryParse(e.NewTextValue, out double number) || number < 0))
                ((Entry) sender).Text = e.OldTextValue;
        }

        private void GoToNextPage(string titleText, string currentCostText, string previousCostText)
        {
            ExceptionChecker.CheckValidParams(titleText, currentCostText, previousCostText);
            Deal currentDeal = new Deal(
                foodPlaceId: SelectedPlace.Id,
                title: titleText,
                currentCost: double.Parse(currentCostText),
                previousCost: double.Parse(previousCostText)
            );
            //_logger.LogInformation(LogEvents.CreateDeal, "Deal {DealTitle} ({DealId}) created.", currentDeal.Title currentDeal.Id);
            SelectedPlace.Deals.Add(currentDeal);
            Navigation.PushAsync(new NewDealNextPage(currentDeal));
        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(
                () => GoToNextPage(TitleEntry.Text, CurrentCostEntry.Text, RegularCostEntry.Text),
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
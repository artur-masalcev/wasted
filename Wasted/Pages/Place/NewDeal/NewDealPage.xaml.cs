using System;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) &&
                (!double.TryParse(e.NewTextValue,out double number) || number < 0))
                ((Entry) sender).Text = e.OldTextValue;
        }

        public double ParseDouble(string text)
        {
            return string.IsNullOrEmpty(text) ? 0 : double.Parse(text);
        }
        private void NextButtonClicked(object sender, EventArgs e)
        {
            Deal currentDeal = new Deal(
                title:TitleEntry.Text,
                currentCost:ParseDouble(CurrentCostEntry.Text),
                previousCost:ParseDouble(RegularCostEntry.Text)
            );
            Navigation.PushAsync(new NewDealNextPage(currentDeal, SelectedPlace));
        }
    }
}
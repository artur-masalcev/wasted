using System;
using Wasted.Utils;
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
        private string TitleText => TitleEntry.Text;
        private string CurrentCostText => CurrentCostEntry.Text;
        private string RegularCostText => RegularCostEntry.Text;

        public NewDealPage(FoodPlace selectedPlace)
        {
            SelectedPlace = selectedPlace;
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
        }

        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!NumberParser.IsValidNumberString(e.NewTextValue))
                ((Entry) sender).Text = e.OldTextValue;
        }
        

        private void NextButtonClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                Deal currentDeal = new Deal(
                    foodPlaceId: SelectedPlace.Id,
                    title: TitleText,
                    currentCost: double.Parse(CurrentCostText),
                    previousCost: double.Parse(RegularCostText) //TODO: make consistent property names
                );
                SelectedPlace.Deals.Add(currentDeal);
                Navigation.PushAsync(new NewDealNextPage(currentDeal));
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(TitleText, CurrentCostText, RegularCostText);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
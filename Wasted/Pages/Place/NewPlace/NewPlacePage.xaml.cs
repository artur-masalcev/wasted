using System;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Place.NewPlace;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPlacePage : ContentPage
    {
        public EntryLengthValidator Validator { get; set; }
        public FoodPlace CurrentFoodPlace { get; set; }
        public NewPlacePage()
        {
            InitializeComponent();
            Validator = new EntryLengthValidator(maxEntryLength: 50);
            BindingContext = Validator;
            CityPicker.ItemsSource = new[] {"Vilnius"};
            
            On<iOS>().SetUseSafeArea(true);
        }
        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }

        private void GoToNextPage(string titleText, string selectedCity, string descriptionEntryText)
        {
            ExceptionChecker.CheckValidParams(titleText, selectedCity, descriptionEntryText);
            CurrentFoodPlace = new FoodPlace(
                title:titleText,
                city:selectedCity,
                description:descriptionEntryText
            );
            Navigation.PushAsync(new NewPlaceImagePage(CurrentFoodPlace));
        }
        private void NextButtonClicked(object sender, EventArgs e)
        {
            ExceptionHandler.WrapFunctionCall(
                () => GoToNextPage(TitleEntry.Text, (string)CityPicker.SelectedItem, DescriptionEntry.Text),
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
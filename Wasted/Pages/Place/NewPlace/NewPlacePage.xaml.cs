using System;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place.NewPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPlacePage : ContentPage
    {
        public EntryLengthValidator Validator { get; set; }
        public FoodPlace CurrentFoodPlace { get; set; }

        private String TitleText => TitleEntry.Text;
        private String SelectedCity => (string) CityPicker.SelectedItem;
        private String DescriptionText => DescriptionEntry.Text;

        public NewPlacePage()
        {
            InitializeComponent();
            Validator = new EntryLengthValidator(maxEntryLength: 50);
            BindingContext = Validator;
            CityPicker.ItemsSource = new[] {"Vilnius"}; //TODO: extract

            On<iOS>().SetUseSafeArea(true);
        }

        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            if (ValidParams())
            {
                CurrentFoodPlace = new FoodPlace(
                    title: TitleText,
                    city: SelectedCity,
                    description: DescriptionText
                );
                Navigation.PushAsync(new NewPlaceImagePage(CurrentFoodPlace));
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(TitleText, SelectedCity, DescriptionText);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
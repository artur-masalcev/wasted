using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Place.NewPlace;
using Wasted.Utils;
using Xamarin.Forms;
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
            Validator = new EntryLengthValidator(maxEntryLength: 30);
            BindingContext = Validator;
            CityPicker.ItemsSource = new[] {"Vilnius"};
        }
        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }
        private void NextButtonClicked(object sender, EventArgs e)
        {
            CurrentFoodPlace = new FoodPlace(
                title:TitleEntry.Text,
                city:(string)CityPicker.SelectedItem,
                description:DescriptionEntry.Text
            );
            Navigation.PushAsync(new NewPlaceImagePage(CurrentFoodPlace));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        public EntryLengthValidator Validator { get; set; }
        
        public DealPage(Deal selectedDeal)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            
            Validator = new EntryLengthValidator(maxEntryLength: 100);
            SelectedDeal = selectedDeal;
            BindingContext = this;
        }
        
        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }

        private void QuantityStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SelectedDeal.Quantity = (int) e.NewValue;
        }

        // TODO: the same code used in NewDealPage, so perhaps this handler can become global(static)
        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) &&
                (!double.TryParse(e.NewTextValue,out double number) || number < 0))
                ((Entry) sender).Text = e.OldTextValue;
        }
        
        private void ChooseImageButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(SelectedDeal, "ImageURL"));
        }

        private void DescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Validator.EntryTextChanged(sender, e);
        }
        
        private void SaveChangesClicked(object sender, EventArgs e)
        {
            var newTitle = TitleEntry.Text;
            var newCurrentCost = CurrentCostEntry.Text;
            var newRegularCost = RegularCostEntry.Text;
            var newDueDate = DueDatePicker.Date.ToString("yyyy-MM-dd");
            var newDescription = DescriptionEntry.Text;

            if (StringUtils.AnyNullOrEmpty(newTitle, 
                newCurrentCost, newRegularCost, newDueDate, newDescription, SelectedDeal.ImageURL))
            {
                DisplayAlert("", "Please fill all fields", "OK");
                return;
            }

            SelectedDeal.Title = newTitle;
            SelectedDeal.DealCosts = new Costs(Convert.ToDouble(newRegularCost), 
                                                Convert.ToDouble(newCurrentCost));
            SelectedDeal.Due = newDueDate;
            SelectedDeal.Description = newDescription;

            //IMPLEMENT ME: Post new deal state to server
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteOfferClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
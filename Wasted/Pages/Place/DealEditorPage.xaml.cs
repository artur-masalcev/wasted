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
    public partial class DealEditorPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        public EntryLengthValidator Validator { get; set; }
        
        private string NewTitle => TitleEntry.Text;
        private string NewCurrentCost => CurrentCostEntry.Text;
        private string NewRegularCost => RegularCostEntry.Text;
        private string NewDueDate => DueDatePicker.Date.ToString("yyyy-MM-dd");
        private string NewDescription => DescriptionEntry.Text;

        public DealEditorPage(Deal selectedDeal)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            
            Validator = new EntryLengthValidator(maxEntryLength: 50);
            SelectedDeal = selectedDeal;
            BindingContext = this;
        }

        /// <summary>
        /// Checks whether all user fields are not empty
        /// </summary>
        /// <returns>'false' if any of fields is empty. 'true' if all fields are not empty</returns>
        private bool IsDataValid()
        {
            return !StringUtils.AnyNullOrEmpty(NewTitle, NewCurrentCost, 
                NewRegularCost, NewDueDate, NewDescription, SelectedDeal.ImageURL);
        }

        /// <summary>
        /// Updates all the fields of 'SelectedDeal' accordingly to user input
        /// </summary>
        /// <notice>
        /// This function does not check if the user input is valid, use 'IsDataValid()' for data validation
        /// </notice>
        private void UpdateSelectedDealObject()
        {
            SelectedDeal.Title = NewTitle;
            SelectedDeal.DealCosts = new Costs(Convert.ToDouble(NewRegularCost), 
                Convert.ToDouble(NewCurrentCost));
            SelectedDeal.Due = NewDueDate;
            SelectedDeal.Description = NewDescription;
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

        /// <summary>
        /// Publishes updated deal data to the server.
        /// Also checks whether the data is valid, if it is not - alert is displayed
        /// </summary>
        private void SaveChangesClicked(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                DisplayAlert("", "Please fill all fields", "OK");
                return;
            }

            UpdateSelectedDealObject();

            //IMPLEMENT ME: Post new deal state (as 'SelectedDeal') to server
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                DisplayAlert("", "Please fill all fields", "OK");
                return;
            }
            
            UpdateSelectedDealObject();
            
            Navigation.PushAsync(new DealPreviewPage(SelectedDeal));
        }

        private void DeleteOfferClicked(object sender, EventArgs e)
        {
            // IMPLEMENT ME
            throw new NotImplementedException();
        }
    }
}
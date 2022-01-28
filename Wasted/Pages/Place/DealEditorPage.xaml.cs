using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Wasted.Utils;
using Wasted.Utils.Exceptions;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
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
        private string NewDueDate => DueDatePicker.Date.ToString(Constants.DateFormat);
        private string NewDescription => DescriptionEntry.Text;

        private UserDetailsService _detailsService = DependencyService.Get<UserDetailsService>();

        public DealEditorPage(Deal selectedDeal)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            Validator = new EntryLengthValidator(maxEntryLength: 50);
            SelectedDeal = selectedDeal;
            BindingContext = this;
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
            SelectedDeal.CurrentCost = Convert.ToDouble(NewCurrentCost);
            SelectedDeal.PreviousCost = Convert.ToDouble(NewRegularCost);
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

        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!NumberParser.IsValidNumberString(e.NewTextValue))
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
            if (ValidParams())
            {
                UpdateSelectedDealObject();
                DealsProvider.UpdateDeal(SelectedDeal);
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private void ShowPreviewClicked(object sender, EventArgs e)
        {
            if (ValidParams()) {
                UpdateSelectedDealObject();
                Navigation.PushAsync(new DealPreviewPage(SelectedDeal));
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(NewTitle, NewCurrentCost,
                NewRegularCost, NewDueDate, SelectedDeal.ImageURL);
        }


        private void DeleteOfferClicked(object sender, EventArgs e)
        {
            DealsProvider.DeleteDeal(SelectedDeal);
            Navigation.PopAsync();
        }
    }
}
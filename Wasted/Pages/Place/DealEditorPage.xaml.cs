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
        private string NewDueDate => DueDatePicker.Date.ToString("yyyy-MM-dd");
        private string NewDescription => DescriptionEntry.Text;

        private CurrentUserService _service = DependencyService.Get<CurrentUserService>();

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
            try
            {
                ExceptionChecker.CheckValidParams(NewTitle, NewCurrentCost,
                    NewRegularCost, NewDueDate, SelectedDeal.ImageURL);
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return true;
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
            SelectedDeal.PreviousCost = Convert.ToDouble(NewCurrentCost);
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
                (!double.TryParse(e.NewTextValue, out double number) || number < 0))
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

            DataProvider.UpdateDeal(SelectedDeal);
            _service.UpdateUserInfo();
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
            DataProvider.DeleteDeal(SelectedDeal);
            _service.UpdateUserInfo();
            Navigation.PopAsync();
        }
    }
}
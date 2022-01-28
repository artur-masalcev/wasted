using System;
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

namespace Wasted.Pages.Place.NewDeal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDealNextPage : ContentPage
    {
        public EntryLengthValidator Validator { get; set; }
        public Deal CurrentDeal { get; set; }

        private string DescriptionText => DescriptionEntry.Text;

        public NewDealNextPage(Deal currentDeal)
        {
            CurrentDeal = currentDeal;
            Validator = new EntryLengthValidator(maxEntryLength: 100);
            BindingContext = Validator;
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);
        }

        private void ImageButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChooseImagePopup(CurrentDeal, "ImageURL"));
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            string dealExpirationDate = DueDatePicker.Date.ToString(Constants.DateFormat);
            if (ValidParams())
            {
                CurrentDeal.Due = dealExpirationDate; //TODO: make not string
                CurrentDeal.Description = DescriptionText;
                DealsProvider.CreateDeal(CurrentDeal);
                PageManager.PushPlacePage(this);
            }
            else
            {
                this.DisplayFillFieldsAlert();
            }
        }

        private bool ValidParams()
        {
            return ParamsChecker.ValidParams(DescriptionText, CurrentDeal.ImageURL);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
using Rg.Plugins.Popup.Services;
using System;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Deals;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        public ItemsPage(Deal selectedDeal)
        {
            SelectedDeal = selectedDeal;
           
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeViews();
        }

        /// <summary>
        /// Sets binding for xaml file. Removes deal stepper if it is already taken
        /// </summary>
        public void InitializeViews()
        {
            content.BindingContext = SelectedDeal;
        }

        /// <summary>
        /// Checks if there are available deals.
        /// </summary>
        private void OnOrderClicked(object sender, EventArgs e)
        {
            if (SelectedDeal.Quantity > 0)
            {
                PopupNavigation.Instance.PushAsync(new OrderPopup(SelectedDeal));
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("No orders left!", new TimeSpan(3));
            }
        }
        
    }
}
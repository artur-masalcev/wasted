using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Deals;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
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

            On<iOS>().SetUseSafeArea(true);

            BindingContext = SelectedDeal;

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
                UserDialogs.Instance.Toast("No orders left!", new TimeSpan(3));
            }
        }
        
    }
}
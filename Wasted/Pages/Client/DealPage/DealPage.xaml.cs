using System;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client.DealPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        private readonly UserDetailsService _detailsService;

        public ItemsPage(Deal selectedDeal)
        {
            SelectedDeal = selectedDeal;

            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = SelectedDeal;

            _detailsService = DependencyService.Get<UserDetailsService>();
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

        /// <summary>
        /// Refreshes the page on scroll down.
        /// </summary>
        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            SelectedDeal = DataProvider.GetAllDeals().Find(deal => deal.Id == SelectedDeal.Id);
            BindingContext = SelectedDeal;
            RefreshView.IsRefreshing = false;
        }
    }
}
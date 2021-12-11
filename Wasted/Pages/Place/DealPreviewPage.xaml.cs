using System;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealPreviewPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        
        public DealPreviewPage(Deal selectedDeal)
        {
            InitializeComponent();

            SelectedDeal = selectedDeal;

            BindingContext = SelectedDeal;

            On<iOS>().SetUseSafeArea(true);
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
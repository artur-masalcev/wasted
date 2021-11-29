using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealPage : ContentPage
    {
        public Deal SelectedDeal { get; set; }
        
        public DealPage(Deal selectedDeal)
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = SelectedDeal;
             
            DisplayAlert("", SelectedDeal.Title, "OK");
            
            SelectedDeal = selectedDeal;
        }
        
        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
    }
}
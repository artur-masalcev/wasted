using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Place.NewDeal;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealsNavigationPage : ContentPage
    {
        private readonly FoodPlace SelectedPlace;
        public string FoodPlaceTitle => SelectedPlace.Title;

        public List<Deal> Deals => SelectedPlace.Deals;

        public DealsNavigationPage(FoodPlace selectedPlace)
        {
            InitializeComponent();

            SelectedPlace = selectedPlace;

            BindingContext = this;

            On<iOS>().SetUseSafeArea(true);
        }
        private void DealsCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deal selectedDeal = e.CurrentSelection.First() as Deal;
            Navigation.PushAsync(new DealPage(selectedDeal));
        }
        
        private void BackClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            base.OnBackButtonPressed();
        }
        
        private void AddNewDealClicked(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new NewDealPage(SelectedPlace));
        }
    }
}
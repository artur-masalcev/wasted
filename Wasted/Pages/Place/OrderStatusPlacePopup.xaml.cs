using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPlacePopup : PopupPage
    {
        public OrderedDeal SelectedDeal { get; set; }

        public string DealTitle => SelectedDeal.Title;
        public int Quantity => SelectedDeal.Quantity;
        
        public string Message{

            get {
                return string.Format("Deal {0} (x{1}) is done preparing",  DealTitle, Quantity);
            }
        }
        
        public OrderStatusPlacePopup(OrderedDeal deal)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = "Ready to pick up";
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}
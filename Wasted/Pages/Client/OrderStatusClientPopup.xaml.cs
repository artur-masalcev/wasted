using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusClientPopup : PopupPage
    {
        public OrderedDeal SelectedDeal { get; set; }
        private DataService service;
        public string Title => SelectedDeal.Title;

        public OrderStatusClientPopup(OrderedDeal deal)
        {
            InitializeComponent();
            SelectedDeal = deal;
            BindingContext = this;
            service = DependencyService.Get<DataService>();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            SelectedDeal.Status = "Received";
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}
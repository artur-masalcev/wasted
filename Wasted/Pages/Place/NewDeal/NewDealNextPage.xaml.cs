using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place.NewDeal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDealNextPage : ContentPage
    {
        private Deal CurrentDeal { get; set; }
        public NewDealNextPage(Deal CurrentDeal)
        {
            InitializeComponent();
        }
    }
}
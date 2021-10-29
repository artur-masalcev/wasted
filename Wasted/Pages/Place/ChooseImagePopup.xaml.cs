using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseImagePopup : PopupPage
    {
        public FoodPlace CurrentPlace { get; set; }
        public PropertyInfo Property { get; set; }
        public ChooseImagePopup(FoodPlace place, string property)
        {
            CurrentPlace = place;
            InitializeComponent();
            Property = typeof(FoodPlace).GetProperty(property);
            URLEntry.Text = (string)Property?.GetValue(CurrentPlace, null);
        }

        private void ImageButtonClicked(object sender, EventArgs e)
        {
            Property?.SetValue(CurrentPlace, URLEntry.Text);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
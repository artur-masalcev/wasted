using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseImagePopup : PopupPage
    {
        public ImageChooserInterface CurrentPlace { get; set; }
        public PropertyInfo Property { get; set; }
        public ChooseImagePopup(ImageChooserInterface obj, string property)
        {
            CurrentPlace = obj;
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
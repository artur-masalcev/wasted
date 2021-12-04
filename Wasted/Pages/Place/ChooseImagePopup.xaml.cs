using System;
using System.Reflection;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseImagePopup : PopupPage
    {
        public IImageChooser Chooser { get; set; }
        public PropertyInfo Property { get; set; }
        public ChooseImagePopup(IImageChooser obj, string property)
        {
            Chooser = obj;
            InitializeComponent();
            Property = typeof(FoodPlace).GetProperty(property);
            URLEntry.Text = (string)Property?.GetValue(Chooser, null);
        }

        /// <summary>
        /// Sets value of passed property in constructor
        /// </summary>
        private void ImageButtonClicked(object sender, EventArgs e)
        {
            Property?.SetValue(Chooser, URLEntry.Text);
            PopupNavigation.Instance.PopAsync();
        }
        
        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
        }
    }
}
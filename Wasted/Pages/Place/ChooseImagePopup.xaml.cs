using System;
using System.Reflection;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils.Interfaces;
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
            Property = obj.GetType().GetProperty(property);
            UrlEntry.Text = (string) Property?.GetValue(Chooser, null);
        }

        /// <summary>
        /// Sets value of passed property in constructor
        /// </summary>
        private void ImageButtonClicked(object sender, EventArgs e)
        {
            Property?.SetValue(Chooser, UrlEntry.Text);
            PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
        }
    }
}
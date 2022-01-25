using Xamarin.Forms;

namespace Wasted.Utils
{
    public static class PageExtensions
    {
        public static void DisplayFillFieldsAlert(this Page page)
        {
            page.DisplayAlert("", "Please fill all fields", "OK");
        }
    }
}
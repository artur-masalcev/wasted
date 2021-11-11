using Wasted.Pages.Place;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class PageManager
    {
        public static void PushClientPage(ContentPage page)
        {
            page.Navigation.PushAsync(new MainPage());
        }

        public static void PushPlacePage(ContentPage page)
        {
            page.Navigation.PushAsync(new PlaceMainPage());
        }
    }
}
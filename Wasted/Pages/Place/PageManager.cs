using Wasted.Pages.Client;
using Xamarin.Forms;

namespace Wasted.Pages.Place
{
    public static class PageManager
    {
        public static void PushClientPage(ContentPage page)
        {
            page.Navigation.PushAsync(new Client.MainPage());
        }

        public static void PushPlacePage(ContentPage page)
        {
            page.Navigation.PushAsync(new MainPage());
        }
    }
}
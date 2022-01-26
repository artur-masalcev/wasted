using Wasted.Pages.Client;
using Xamarin.Forms;

namespace Wasted.Pages.Place
{
    public static class PageManager
    {
        public static void PushClientPage(Page page)
        {
            page.Navigation.PushAsync(new Client.MainPage());
        }

        public static void PushPlacePage(Page page)
        {
            page.Navigation.PushAsync(new MainPage());
        }
    }
}
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class UserPlace : User
    {
        public override void PushPage(ContentPage page)
        {
            PageManager.PushPlacePage(page);
        }

        public override void CreateUser()
        {
            App.PlaceUsers[Username] = this;
        }
    }
}
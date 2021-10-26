using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class UserClient : User
    {
        public override void PushPage(ContentPage page)
        {
            PageManager.PushClientPage(page);
        }

        public override void CreateUser()
        {
            App.ClientUsers[Username] = this;
        }
    }
}
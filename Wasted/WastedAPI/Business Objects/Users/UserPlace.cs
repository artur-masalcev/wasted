using System.Collections.Generic;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class UserPlace : User
    {
        public List<int> OwnedPlaceIDs { get; set; }

        public UserPlace(string username = null, string password = null, string name = null,
            string surname = null, string city = null, string address = null, Dictionary<int, int> ratings = null,
            List<int> ownedPlaceIDs = null) :
            base(username, password, name, surname, city, address, ratings)
        {
            OwnedPlaceIDs = ownedPlaceIDs ?? new List<int>();
        }

        public override void PushPage(ContentPage page)
        {
            PageManager.PushPlacePage(page);
        }

        public override void CreateUser(DataService service)
        {
            service.PlaceUsers[Username] = this;
            DataProvider.WriteAllUserPlaces();
        }
    }
}
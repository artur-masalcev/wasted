using System.Collections.Generic;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class UserPlace : User
    {
        public List<int> OwnedPlaceIDs { get; set; }

        protected UserPlace(string username, string password, string name, string surname,
            string city, string address, Dictionary<int, int> ratings, List<int> ownedPlaceIDs) :
            base(username, password, name, surname, city, address, ratings)
        {
            OwnedPlaceIDs = ownedPlaceIDs ?? new List<int>();
        }
        public UserPlace()
        {
        }
        public override void PushPage(ContentPage page)
        {
            PageManager.PushPlacePage(page);
        }

        public override void CreateUser(DataService service)
        {
            service.PlaceUsers[Username] = this;
        }
    }
}
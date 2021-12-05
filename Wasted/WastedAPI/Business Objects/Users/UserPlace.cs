using System.Collections.Generic;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public class UserPlace : User
    {
        public List<FoodPlace> OwnedPlaces { get; set; }

        public UserPlace(string username = null, string password = null, string name = null,
            string surname = null, string city = null, string address = null,
            List<FoodPlace> foodPlaces = null, int id = default) :
            base(username, password, name, surname, city, address, id)
        {
            OwnedPlaces = foodPlaces ?? new List<FoodPlace>();
        }

        public override void PushPage(ContentPage page)
        {
            PageManager.PushPlacePage(page);
        }

        public override void CreateUser(DataService service)
        {
            // service.PlaceUsers[Username] = this;
            // DataProvider.WriteAllUserPlaces();
        }
    }
}
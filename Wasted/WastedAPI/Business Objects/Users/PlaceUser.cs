using System.Collections.Generic;
using Wasted.Pages.Place;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wasted.WastedAPI.Business_Objects.Users
{
    public class PlaceUser : User
    {
        public List<FoodPlace> OwnedPlaces { get; set; }

        public PlaceUser(string username = null, string password = null, string name = null,
            string surname = null, string city = null, string address = null,
            List<FoodPlace> foodPlaces = null, int id = default) :
            base(username, password, name, surname, city, address, id)
        {
            OwnedPlaces = foodPlaces ?? new List<FoodPlace>();
        }

        public override void InitializePage(Page page)
        {
            PageManager.PushPlacePage(page);
        }

        public override void CreateUser()
        {
            DataProvider.CreatePlaceUser(this);
        }
    }
}
using System.Collections.Generic;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(DataService))]
namespace Wasted.Utils
{
    public class DataService
    {

        public User CurrentUser { get; set; }
        private List<FoodPlace> allFoodPlaces;
        public List<FoodPlace> AllFoodPlaces
        {
            get { return allFoodPlaces; }
            set { allFoodPlaces = BusinessUtils.SortByID<FoodPlace>(value.ToArray()); }
        }

        private List<Deal> allDeals;
        public List<Deal> AllDeals
        {
            get { return allDeals;}
            set { allDeals = BusinessUtils.SortByID<Deal>(value.ToArray()); }
        }

        public List<CartDeal> CartDeals { get; set; } = new List<CartDeal>();
        public List<OrderedDeal> OrderedDeals { get; set; } = new List<OrderedDeal>();
        
        //                username
        public Dictionary<string, UserClient> ClientUsers { get; set; }
        //                username
        public Dictionary<string, UserPlace> PlaceUsers { get; set; }
        
        public Location UserLocation { get; set; }
    }
}

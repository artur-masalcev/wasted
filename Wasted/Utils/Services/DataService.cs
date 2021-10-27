using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;

[assembly:Dependency(typeof(DataService))]
namespace Wasted.Utils
{
    public class DataService
    {
        public User CurrentUser { get; set; }
        public List<FoodPlace> AllFoodPlaces { get; set; }
        public List<Deal> AllDeals { get; set; }
        //                username
        public Dictionary<string, UserClient> ClientUsers { get; set; }
        //                username
        public Dictionary<string, UserPlace> PlaceUsers { get; set; }
        public HttpClient Client { get; set; }
    }
}

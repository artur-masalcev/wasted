using System.Collections.Generic;
using Wasted;

namespace DataAPI.Models.Users
{
    public class PlaceUser : AbstractUser
    {
        public override string UserType => Constants.PlaceType;
        public List<FoodPlace> FoodPlaces { get; set; }
    }
}
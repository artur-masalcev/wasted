using System.Collections.Generic;

namespace DataAPI.Models.Users
{
    public class PlaceUser : AbstractUser
    {
        public List<FoodPlace> FoodPlaces { get; set; }
    }
}
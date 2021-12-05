using System.Collections.Generic;

namespace DataAPI.Models
{
    public class PlaceUser : AbstractUser
    {
        public List<FoodPlace> FoodPlaces { get; set; }
    }
}
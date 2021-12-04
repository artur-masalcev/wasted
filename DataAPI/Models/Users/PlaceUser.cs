using System.Collections.Generic;

namespace DataAPI.Models
{
    public class PlaceUser : AbstractUser
    {
        public int Id { get; set; }
        public List<FoodPlace> FoodPlaces { get; set; }
    }
}
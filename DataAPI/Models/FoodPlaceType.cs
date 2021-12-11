using System.Collections.Generic;

namespace DataAPI.Models
{
    public class FoodPlaceType
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<FoodPlace> FoodPlaces { get; set; }
    }
}
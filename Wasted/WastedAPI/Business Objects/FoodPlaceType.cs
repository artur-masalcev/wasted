using System.Collections.Generic;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlaceType
    {
        public string Type { get; set; }
        public List<FoodPlace> FoodPlaces { get; set; }

        public FoodPlaceType(string value = null, List<FoodPlace> foodPlaces = null)
        {
            Type = value;
            FoodPlaces = foodPlaces;
        }
    }
}
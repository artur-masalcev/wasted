using System.Collections.Generic;

namespace DataAPI.DTO
{
    public class FoodPlaceTypeDTO
    {
        public string Value { get; set; }
        public List<FoodPlaceDTO> FoodPlaces { get; set; }
    }
}
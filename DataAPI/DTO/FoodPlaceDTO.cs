using System.Collections.Generic;

namespace DataAPI.DTO
{
    public class FoodPlaceDTO
    {
        public string Name { get; set; }
        public List<RatingDTO> Ratings { get; set; }
        public List<DealDTO> Deals { get; set; }
    }
}
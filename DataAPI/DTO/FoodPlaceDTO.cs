using System.Collections.Generic;

namespace DataAPI.DTO
{
    public class FoodPlaceDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public string PlaceType { get; set; }
        public string Street { get; set; } //Never used
        public string City { get; set; } //Never used
        public string WorkingHours { get; set; } //Never used
        
        public List<RatingDTO> Ratings { get; set; }
        public List<DealDTO> Deals { get; set; }
    }
}
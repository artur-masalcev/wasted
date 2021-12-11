using System.Collections.Generic;

namespace DataAPI.DTO
{
    public class FoodPlaceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public string Street { get; set; } //Never used
        public string City { get; set; } //Never used
        public string WorkingHours { get; set; } //Never used

        public string PlaceTypeValue { get; set; }

        public List<RatingDTO> Ratings { get; set; }
        public List<DealDTO> Deals { get; set; }
    }
}
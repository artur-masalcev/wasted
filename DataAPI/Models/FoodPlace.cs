using System.Collections.Generic;
using DataAPI.Models.Users;

namespace DataAPI.Models
{
    public class FoodPlace
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

        public int FoodPlaceTypeId { get; set; }
        public FoodPlaceType PlaceType { get; set; }
        public int PlaceUserId { get; set; }
        public PlaceUser PlaceUser { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Deal> Deals { get; set; }
    }
}
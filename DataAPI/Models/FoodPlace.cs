using System.Collections.Generic;

namespace DataAPI.Models
{
    public class FoodPlace
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public string PlaceType { get; set; }
        public string Street { get; set; } //Never used
        public string City { get; set; } //Never used
        public string WorkingHours { get; set; } //Never used
        public List<Rating> Ratings { get; set; }
        public List<Deal> Deals { get; set; }
    }
}
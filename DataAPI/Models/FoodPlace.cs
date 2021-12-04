using System.Collections.Generic;

namespace DataAPI.Models
{
    public class FoodPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Deal> Deals { get; set; }
    }
}
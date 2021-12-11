using DataAPI.Models.Users;

namespace DataAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public double Value { get; set; }

        public int FoodPlaceId { get; set; }
        public FoodPlace FoodPlace { get; set; }

        public int ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
    }
}
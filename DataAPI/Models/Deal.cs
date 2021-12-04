namespace DataAPI.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int FoodPlaceId { get; set; }
        public FoodPlace FoodPlace { get; set; }
    }
}
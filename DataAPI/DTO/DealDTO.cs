
namespace DataAPI.DTO
{
    public class DealDTO
    {
        public string Title { get; set; }
        public double PreviousCost { get; set; }
        public double CurrentCost { get; set; }
        public int Quantity { get; set; }
        public string Due { get; set; } //Lol string
        public string ImageURL { get; set; }
        public string Description { get; set; }
        
        public string FoodPlaceTitle { get; set; }
    }
}
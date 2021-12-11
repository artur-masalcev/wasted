namespace DataAPI.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double PreviousCost { get; set; }
        public double CurrentCost { get; set; }
        public int Quantity { get; set; }
        public string Due { get; set; } //Lol string
        public string ImageURL { get; set; }
        public string Description { get; set; }

        public int FoodPlaceId { get; set; }
        public FoodPlace FoodPlace { get; set; }
    }
}
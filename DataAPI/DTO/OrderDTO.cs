namespace DataAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public long TimeLeft { get; set; }
        public int PreparationTime{ get; set; }
        public long StartTime { get; set; }
        
        public int DealId { get; set; }
        public int ClientUserId { get; set; }
        public int DealFoodPlacePlaceUserId { get; set; }
        public string DealTitle { get; set; }
        public double DealCurrentCost { get; set; }
    }
}
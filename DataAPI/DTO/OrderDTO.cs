namespace DataAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        
        public int DealId { get; set; }
        public int ClientUserId { get; set; }
        public int DealFoodPlacePlaceUserId { get; set; }
    }
}
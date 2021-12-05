namespace DataAPI.DTO
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int ClientUserId { get; set; }
        public int FoodPlaceId { get; set; }
        public double Value { get; set; }

        public RatingDTO(int clientUserId, int foodPlaceId, double value)
        {
            ClientUserId = clientUserId;
            FoodPlaceId = foodPlaceId;
            Value = value;
        }
    }
}
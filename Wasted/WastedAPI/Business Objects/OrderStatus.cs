namespace Wasted.WastedAPI.Business_Objects
{
    public static class OrderStatus
    {
        public const string InCart = "In cart";
        public const string Preparing = "Preparing";
        public const string ReadyToPickUp = "Ready to pick up";
        public const string Received = "Received";
    }

    public class OrderStatusSummary
    {
        public string Status { get; set; }
        public int Quantity { get; set; }
        public OrderStatusSummary(string status, int quantity)
        {
            Status = status;
            Quantity = quantity;
        }
        
        public override string ToString()
        {
            return $"{Status}: {Quantity}";
        }
    }
}
namespace DataAPI.Logging
{
    public class LogEvents
    {
        // User events
        public const int UserLoggedIn = 1001;
    
        // Post events
        public const int CreateDeal = 2001;
        public const int CreatePlace = 2002;
    
        // Order events
        public const int DealPurchased = 3001;
        public const int OrderAccepted = 3002;
        public const int OrderPrepared = 3003;
        public const int OrderCompleted = 3004;
    }
}
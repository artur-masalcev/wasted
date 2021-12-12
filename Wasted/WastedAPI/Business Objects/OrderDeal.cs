using System;
using Wasted.Utils;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderDeal : ChangeablePropertyObject
    {
        public int Id { get; set; }
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public int DealId { get; set; }

        public int Quantity { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        
        private long timeLeft;

        public long TimeLeft
        {
            get { return timeLeft; }
            set
            {
                long totalMinutes = value - ((long) TimeSpan
                    .FromMilliseconds(DateTimeOffset.Now.ToUnixTimeMilliseconds() - StartTime).TotalMinutes);
                timeLeft = totalMinutes > 0 ? totalMinutes : -1;
            }
        }

        public int PreparationTime{ get; set; }
        
        public long StartTime { get; set; }
        public int PlaceUserId { get; set; }
        public int ClientUserId { get; set; }

        public OrderDeal(int dealId = default, string status = null, int quantity = default, int clientUserId = default,
            int dealFoodPlacePlaceUserId = default, string dealTitle = null, double dealCurrentCost = default)
        {
            DealId = dealId;
            Title = dealTitle;
            Quantity = quantity;
            ClientUserId = clientUserId;
            Cost = quantity * dealCurrentCost;
            Status = status;
            Quantity = quantity;
            PlaceUserId = dealFoodPlacePlaceUserId;
        }
    }
}
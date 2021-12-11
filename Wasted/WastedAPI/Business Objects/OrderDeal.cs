using Wasted.Utils;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderDeal : ChangeablePropertyObject
    {
        public Deal PurchasedDeal { get; set; }
        
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

        public int Quantity { get; set; }
        
        public double Cost => Quantity * PurchasedDeal.CurrentCost;
        public string Title => PurchasedDeal.Title;

        public OrderDeal(Deal purchasedDeal, string status, int quantity)
        {
            PurchasedDeal = purchasedDeal;
            Status = status;
            Quantity = quantity;
        }
    }
}
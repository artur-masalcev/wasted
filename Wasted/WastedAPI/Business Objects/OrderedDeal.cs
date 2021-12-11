using Wasted.Utils;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderedDeal : ChangeablePropertyObject
    {
        public CartDeal PurchasedDeal { get; set; }

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

        public string Title => PurchasedDeal.Title;

        public OrderedDeal(CartDeal purchasedDeal, string status, int quantity)
        {
            PurchasedDeal = purchasedDeal;
            Status = status;
            Quantity = quantity;
        }
    }
}
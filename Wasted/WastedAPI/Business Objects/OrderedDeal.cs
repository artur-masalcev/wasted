using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;

namespace Wasted.Dummy_API.Business_Objects
{
    public class OrderedDeal : ChangeablePropertyObject
    {
        public CartDeal PurchasedDeal { get; set; }

        private string status;

        public string Status
        {
            get => status;
            set
            {
                status = value;
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
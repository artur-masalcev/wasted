using Wasted.WastedAPI.Business_Objects;

namespace Wasted.Dummy_API.Business_Objects
{
    public class OrderedDeal : ChangeablePropertyObject
    {
        public CartDeal PurchasedDeal { get; set; }

        private string status;

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public string Title => PurchasedDeal.Title;

        public OrderedDeal(CartDeal purchasedDeal, string status)
        {
            PurchasedDeal = purchasedDeal;
            Status = status;
        }
    }
}
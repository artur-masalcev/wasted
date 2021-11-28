using Wasted.WastedAPI.Business_Objects;

namespace Wasted.Dummy_API.Business_Objects
{
    public class OrderedDeal
    {
        public CartDeal PurchasedDeal;

        public string Status;

        public string Title => PurchasedDeal.Title;

        public OrderedDeal(CartDeal purchasedDeal, string status)
        {
            PurchasedDeal = purchasedDeal;
            Status = status;
        }
    }
}
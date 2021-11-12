using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.WastedAPI
{
    public static class DefaultFilters
    {
        public static bool DealInStock(Deal deal)
        {
            return deal.quantity > 0;
        }
    }
}
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderedDeal
    {
        private Deal SelectedDeal { get; set; }
        private int Quantity { get; set; }
        private string Status { get; set; }
        
        public OrderedDeal(Deal deal, int quantity, string status)
        {
            SelectedDeal = deal;
            Quantity = quantity;
            Status = status;
        }
    }
}
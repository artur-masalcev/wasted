using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderedDeal
    {
        public Deal SelectedDeal { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Title => SelectedDeal.Title;
        
        public OrderedDeal(Deal deal, int quantity, string status)
        {
            SelectedDeal = deal;
            Quantity = quantity;
            Status = status;
        }
    }
}
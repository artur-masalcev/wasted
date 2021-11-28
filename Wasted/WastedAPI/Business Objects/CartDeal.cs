using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.WastedAPI.Business_Objects
{
    public class CartDeal

    {
    public Deal SelectedDeal { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }

    public double Cost { get; set; }
    public string Title => SelectedDeal.Title;

    public CartDeal(Deal deal, int quantity, string status, double cost)
    {
        SelectedDeal = deal;
        Quantity = quantity;
        Status = status;
        Cost = cost;
    }
    }
}
namespace Wasted.WastedAPI.Business_Objects
{
    public class CartDeal
    {
        public Deal SelectedDeal { get; set; }
        public int Quantity { get; set; }

        public double Cost { get; set; }
        public string Title => SelectedDeal.Title;

        public CartDeal(Deal deal, int quantity, double cost)
        {
            SelectedDeal = deal;
            Quantity = quantity;
            Cost = cost;
        }
    }
}
using System;
namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double CurrentCost { get; set; }
        public double PreviousCost { get; set; }
        public int Quantity { get; set; }
        public string Due { get; set; }
        public string ImageURL { get; set; }

        public Deal(int id, string title, double currentCost, double previousCost, int quantity, string due, string imageURL)
        {
            ID = id;
            Title = title;
            CurrentCost = currentCost;
            PreviousCost = previousCost;
            Quantity = quantity;
            Due = due;
            ImageURL = imageURL;
        }
    }
}

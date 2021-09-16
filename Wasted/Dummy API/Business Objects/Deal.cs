using System;
namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal
    {
        public int ID;
        public string Title;
        public double CurrentCost;
        public double PreviousCost;
        public int Quantity;
        public string Due;
        public string ImageURL;

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

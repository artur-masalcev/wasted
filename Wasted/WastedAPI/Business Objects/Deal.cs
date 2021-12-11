using Wasted.Utils;
using Wasted.Utils.Interfaces;

namespace Wasted.WastedAPI.Business_Objects
{
    public class Deal : ChangeablePropertyObject, IImageChooser
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double PreviousCost { get; set; }
        public double CurrentCost { get; set; }
        
        private int quantity = 0;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                //DataProvider.WriteAllDeals();
                OnPropertyChanged();
            }
        }
        public string Due { get; set; }
        public string ImageURL { get; set; }

        public string Description { get; set; }

        public string FoodPlaceTitle { get; set; }
        public int FoodPlaceId { get; set; }

        public Deal(int quantity = default, int id = default, string title = null, double previousCost = default, double currentCost = default, string due = null, string imageUrl = null, string description = null, string foodPlaceTitle = null, int foodPlaceId = default)
        {
            this.quantity = quantity;
            Id = id;
            Title = title;
            PreviousCost = previousCost;
            CurrentCost = currentCost;
            Due = due;
            ImageURL = imageUrl;
            Description = description;
            FoodPlaceTitle = foodPlaceTitle;
            FoodPlaceId = foodPlaceId;
        }
    }
}

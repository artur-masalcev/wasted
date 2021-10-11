using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal : ChangeableProperty
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public Costs DealCosts { get; set; }

        public int quantity = 0;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }
        public string Due { get; set; }
        public string ImageURL { get; set; }

        public string Description { get; set; }

        private int selectedCount = 0;
        public int SelectedCount
        {
            get { return selectedCount; }
            set
            {
                selectedCount = value;
                OnPropertyChanged();
            }
        }

        public List<FoodPlace> FoodPlaces { get; set; } = new List<FoodPlace>();

        public string FoodPlaceTitle
        {
            get { return FoodPlaces.First().Title; }
        }

        public Deal(int id, string title, string description, double currentCost,
            double previousCost, int quantity, string due, string imageURL)
        {
            ID = id;
            Title = title;
            Description = description;
            DealCosts = new Costs(previousCost, currentCost);
            Quantity = quantity;
            Due = due;
            ImageURL = imageURL;

            HashMaps.DealsHashMap[id] = this;
        }
    }
}

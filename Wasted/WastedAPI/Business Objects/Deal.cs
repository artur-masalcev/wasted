using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal : ChangeablePropertyObject, IIntegerIdentifiable, IImageChooser
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

        [JsonIgnore]
        public List<FoodPlace> FoodPlaces { get; set; } = new List<FoodPlace>();

        /// <summary>
        /// Food place which has the item. Can be any from FoodPlaces list
        /// </summary>
        [JsonIgnore]
        public string FoodPlaceTitle => FoodPlaces.First().Title;

        public Deal(int id = 0, string title = null, string description = null, double currentCost = 0,
            double previousCost = 0, int quantity = 0, string due = null, string imageURL = null)
        {
            ID = id;
            Title = title;
            Description = description;
            DealCosts = new Costs(previousCost, currentCost);
            Quantity = quantity;
            Due = due;
            ImageURL = imageURL;
        }
    }
}

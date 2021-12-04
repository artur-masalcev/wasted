﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal : ChangeablePropertyObject, IImageChooser
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double PreviousCost { get; set; }
        public double CurrentCost { get; set; }
        
        public int quantity = 0;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                DataProvider.WriteAllDeals();
                OnPropertyChanged();
            }
        }
        public string Due { get; set; }
        public string ImageURL { get; set; }

        public string Description { get; set; }

        public string FoodPlaceTitle { get; set; } //TODO: change to title

        public Deal(int quantity = default, int id = default, string title = null, double previousCost = default, double currentCost = default, string due = null, string imageUrl = null, string description = null, string foodPlaceTitle = null)
        {
            this.quantity = quantity;
            ID = id;
            Title = title;
            PreviousCost = previousCost;
            CurrentCost = currentCost;
            Due = due;
            ImageURL = imageUrl;
            Description = description;
            FoodPlaceTitle = foodPlaceTitle;
        }
    }
}

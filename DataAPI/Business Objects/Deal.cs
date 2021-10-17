using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class Deal
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public Costs DealCosts { get; set; }

        public int Quantity { get; set; }
        public string Due { get; set; }
        public string ImageURL { get; set; }

        public string Description { get; set; }

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

        }
    }
}

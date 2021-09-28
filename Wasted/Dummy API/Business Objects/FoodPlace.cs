using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Wasted.Dummy_API;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace
    {
        public int ID;
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }
        public double Rating { get; set; }
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public int[] DealsIDs { get; set; }

        private List<Deal> deals = new List<Deal>();
        public List<Deal> Deals
        {
            get { return deals; }
            set { deals = value; }
        }

        public int DealsCount => DealsIDs.Length;

        public FoodPlace(int id, string title, string description, double longitude, double latitude, string street, string city, string workingHours, double rating, string logoURL, string headerURL, int[] dealIDs)
        {
            ID = id;
            Title = title;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Street = street;
            City = city;
            WorkingHours = workingHours;
            Rating = rating;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealsIDs = dealIDs;

            HashMaps.FoodPlacesHashMap[id] = this;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace : ChangeableProperty
    {
        public int ID;
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }

        public int RatingCount { get; set; }

        private double rating;
        public double Rating
        {
            get { return rating; }
            set
            {                
                rating = (value + rating * RatingCount) / (RatingCount + 1);
                ++RatingCount;
                OnPropertyChanged();
            }
        }
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

        public FoodPlace(int id, string title, string description, double longitude, double latitude, string street, string city,
            string workingHours, double rating, int ratingCount, string logoURL, string headerURL, int[] dealIDs)
        {
            ID = id;
            Title = title;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Street = street;
            City = city;
            WorkingHours = workingHours;
            this.rating = rating;
            RatingCount = ratingCount;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealsIDs = dealIDs;

            HashMaps.FoodPlacesHashMap[id] = this;
        }
    }
}

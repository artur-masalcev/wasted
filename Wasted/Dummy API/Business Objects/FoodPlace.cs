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

        public Coordinates Location { get; set; }
  
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

        public List<Deal> Deals = new List<Deal>();

        public int DealsCount => DealsIDs.Length;

        public FoodPlace(int id, string title, int placeType, string description, double longitude, double latitude, string street,
            string city, string workingHours, double rating, int ratingCount, string logoURL, string headerURL, int[] dealIDs)
        {
            ID = id;
            Title = title;
            Description = description;
            Location = new Coordinates(longitude, latitude);
            Street = street;
            City = city;
            WorkingHours = workingHours;
            this.rating = rating;
            RatingCount = ratingCount;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealsIDs = dealIDs;

            if (!HashMaps.FoodPlacesHashMap.ContainsKey(id))
            {
                HashMaps.FoodPlaceTypeHashMap[placeType].Add(this);
                HashMaps.FoodPlacesHashMap[id] = this;
            }  
        }
    }
}

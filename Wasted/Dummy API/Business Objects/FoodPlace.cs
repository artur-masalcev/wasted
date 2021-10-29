using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Essentials;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace : ChangeablePropertyObject, IDInterface
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Location PlaceLocation { get; set; }

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
        public List<int> DealsIDs { get; set; }


        [JsonIgnore] //Prevents infinite recursion when serializing to json file
        public List<Deal> Deals = new List<Deal>();

        [JsonIgnore]
        public int DealsCount => Deals.Count;

        public string PlaceType { get; set; }

        public FoodPlace(int id = 0, string title = null, string placeType = null, string description = null,
            double longitude = 0, double latitude = 0, string street = null, string city = null,
            string workingHours = null, double rating = 0, int ratingCount = 0, string logoURL = null,
            string headerURL = null, List<int> dealsIDs = null)
        {
            ID = id;
            Title = title;
            Description = description;
            PlaceLocation = new Location(longitude, latitude);
            Street = street;
            City = city;
            WorkingHours = workingHours;
            this.rating = rating;
            RatingCount = ratingCount;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealsIDs = dealsIDs == null ? new List<int>() : dealsIDs;
            PlaceType = placeType;
            if (placeType != null)
                HashMaps.PlaceTypeDictionary.PutDefaultKey(placeType, this);
        }
    }
}

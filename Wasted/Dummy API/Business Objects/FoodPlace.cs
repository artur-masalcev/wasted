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
using Xamarin.Essentials;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace : ChangeablePropertyObject
    {
        public int ID;
        public string Title { get; set; }
        public string Description { get; set; }

        public Location PlaceLocation { get; set; }
  
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }

        public int RatingCount { get; set; }

        public double Rating { get; set; }

        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public int[] DealsIDs { get; set; }


        [JsonIgnore] //Prevents infinite recursion when serializing to json file
        public List<Deal> Deals = new List<Deal>();

        public int DealsCount => DealsIDs.Length;

        public int PlaceType { get; set; }

        public FoodPlace(int id, string title, int placeType, string description, double longitude, double latitude, string street,
            string city, string workingHours, double rating, int ratingCount, string logoURL, string headerURL, int[] dealIDs)
        {
            ID = id;
            Title = title;
            Description = description;
            PlaceLocation = new Location(longitude, latitude);
            Street = street;
            City = city;
            WorkingHours = workingHours;
            Rating = rating;
            RatingCount = ratingCount;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealsIDs = dealIDs;

            PlaceType = placeType;
            HashMaps.FoodPlaceTypeHashMap[PlaceType].Add(this);
        }
    }
}

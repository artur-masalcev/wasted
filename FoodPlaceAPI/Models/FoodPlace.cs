using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlaceAPI.Models
{
    public class FoodPlace
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }
        public int RatingCount { get; set; }
        public double Rating { get; set; }
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public string DealIDs { get; set; }
        public int PlaceType { get; set; }

        public FoodPlace()
        {

        }

        public FoodPlace(int id, string title, string description, double longitude, double latitude, string street, string city, string workingHours, int ratingCount, double rating, string logoURL, string headerURL, string dealIDs, int placeType)
        {
            ID = id;
            Title = title;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Street = street;
            City = city;
            WorkingHours = workingHours;
            RatingCount = ratingCount;
            Rating = rating;
            LogoURL = logoURL;
            HeaderURL = headerURL;
            DealIDs = dealIDs;
            PlaceType = placeType;
        }
    }
}
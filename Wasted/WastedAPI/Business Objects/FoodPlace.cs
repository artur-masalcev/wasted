using System.Collections.Generic;
using System.Linq;
using DataAPI.DTO;
using Newtonsoft.Json;
using Wasted.Dummy_API;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Essentials;

namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace : ChangeablePropertyObject, IImageChooser
    {
        public int ID { get; set; }
        public string Name { get; set; } //TODO: rename to title
        public string Description { get; set; }
        public Location PlaceLocation { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }
        public int RatingCount { get; set; }

        public List<RatingDTO> Ratings { get; set; }
        public double Rating => Ratings.Average(rating => rating.Value);
        
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        public List<DealDTO> Deals { get; set; }
        public int DealsCount => Deals.Count;
        public string PlaceType { get; set; }

        // public FoodPlace(int id = 0, string title = null, string placeType = null, string description = null,
        //     double longitude = 0, double latitude = 0, string street = null, string city = null,
        //     string workingHours = null, double rating = 0, int ratingCount = 0, string logoURL = null,
        //     string headerURL = null, List<int> dealsIDs = null)
        // {
        //     ID = id;
        //     Title = title;
        //     Description = description;
        //     PlaceLocation = new Location(longitude, latitude);
        //     Street = street;
        //     City = city;
        //     WorkingHours = workingHours;
        //     RatingCount = ratingCount;
        //     LogoURL = logoURL;
        //     HeaderURL = headerURL;
        //     PlaceType = placeType;
        //     if (placeType != null)
        //         BusinessUtils.PlaceTypeDictionary.PutDefaultKey(placeType, this);
        // }

        public FoodPlace(int id = default, string name = null, string description = null, Location placeLocation = null, string street = null, string city = null, string workingHours = null, int ratingCount = default, List<RatingDTO> ratings = null, string logoUrl = null, string headerUrl = null, List<DealDTO> deals = null, string placeType = null)
        {
            ID = id;
            Name = name;
            Description = description;
            PlaceLocation = placeLocation;
            Street = street;
            City = city;
            WorkingHours = workingHours;
            RatingCount = ratingCount;
            Ratings = ratings;
            LogoURL = logoUrl;
            HeaderURL = headerUrl;
            Deals = deals;
            PlaceType = placeType;
        }
    }
}

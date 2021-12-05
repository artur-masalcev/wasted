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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Location PlaceLocation { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string WorkingHours { get; set; }
        public int RatingCount { get; set; }
        [JsonIgnore]
        public List<RatingDTO> Ratings { get; set; }
        [JsonIgnore]
        public double Rating => Ratings.Average(rating => rating.Value);
        public string LogoURL { get; set; }
        public string HeaderURL { get; set; }
        [JsonIgnore]
        public List<Deal> Deals { get; set; }
        [JsonIgnore]
        public int DealsCount => Deals.Count;
        public string Type { get; set; }
        public int FoodPlaceTypeId { get; set; }
        public int PlaceUserId { get; set; }

        public FoodPlace(int id = default, string title = null, string description = null, long longitude = default, long latitude = default,
            string street = null, string city = null, string workingHours = null, int ratingCount = default, List<RatingDTO> ratings = null,
            string logoUrl = null, string headerUrl = null, List<Deal> deals = null, string placeTypeValue = null)
        {
            Id = id;
            Title = title;
            Description = description;
            PlaceLocation = new Location(longitude, latitude);
            Street = street;
            City = city;
            WorkingHours = workingHours;
            RatingCount = ratingCount;
            Ratings = ratings;
            LogoURL = logoUrl;
            HeaderURL = headerUrl;
            Deals = deals ?? new List<Deal>();
            Type = placeTypeValue;
        }
    }
}

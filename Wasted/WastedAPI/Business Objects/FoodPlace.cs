using System.Collections.Generic;
using System.Linq;
using DataAPI.DTO;
using Newtonsoft.Json;
using Wasted.Dummy_API.Business_Objects;
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
        [JsonIgnore]
        public List<RatingDTO> Ratings { get; set; }
        public double Rating => Ratings.Count == 0 ? 0 : Ratings.Average(r => r.Value);
        public void RatingChanged() => OnPropertyChanged("Rating");

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
            string street = null, string city = null, string workingHours = null, List<RatingDTO> ratings = null,
            string logoUrl = null, string headerUrl = null, List<Deal> deals = null, string placeTypeValue = null)
        {
            Id = id;
            Title = title;
            Description = description;
            PlaceLocation = new Location(longitude, latitude);
            Street = street;
            City = city;
            WorkingHours = workingHours;
            Ratings = ratings ?? new List<RatingDTO>();
            LogoURL = logoUrl;
            HeaderURL = headerUrl;
            Deals = deals ?? new List<Deal>();
            Type = placeTypeValue;
        }
    }
}

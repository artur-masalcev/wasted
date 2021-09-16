using System;
namespace Wasted.DummyAPI.BusinessObjects
{
    public class FoodPlace
    {
        public int ID;
        public string Title;
        public string Description;
        public double Longitude;
        public double Latitude;
        public string Street;
        public string City;
        public string WorkingHours;
        public double Rating;
        public string LogoURL;
        public string HeaderURL;
        public int[] DealsIDs;

        public FoodPlace(int id, string title, string description, double longitude, double latitude, string street, string city, string workingHours, double rating, string logoURL, string headerURL, int[] dealsIDs)
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
            DealsIDs = dealsIDs;
        }
    }
}

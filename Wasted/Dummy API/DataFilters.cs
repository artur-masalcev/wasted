using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Wasted.DummyAPI
{
    public class DataFilters
    {
        /// <summary>
        /// Sorts food places by the location to the user.
        /// </summary>
        public static IEnumerable<FoodPlace> GetNearbyPlaces(List<FoodPlace> allFoodPlaces, Location UserLocation, int maxKilometers)
        {
            Func<Location, FoodPlace, double> GetDistance =
                (location, place) => Location.CalculateDistance(location, place.PlaceLocation, DistanceUnits.Kilometers);

            return from place in allFoodPlaces
                   orderby GetDistance(UserLocation, place)
                   where GetDistance(UserLocation, place) <= maxKilometers
                   select place;
        }

        /// <summary>
        /// Sorts deals by the percentage of change in cost. Takes the first 'offerCount' of them.
        /// </summary>
        public static IEnumerable<Deal> GetSpecialOffers(List<Deal> allDeals, int offerCount)
        {
            return (
                   from deal in allDeals
                   orderby deal.DealCosts.PriceChange()
                   select deal
                   )
                   .Take(offerCount);
        }

        /// <summary>
        /// Sorts food places by the number of ratings.
        /// </summary>
        public static IEnumerable<FoodPlace> GetPopularFoodPlaces(List<FoodPlace> allFoodPlaces, int offerCount)
        {
            return (
                   from place in allFoodPlaces
                   orderby -place.RatingCount
                   select place
                   )
                   .Take(offerCount);
        }
    }
}

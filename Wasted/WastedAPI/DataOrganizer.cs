using System;
using System.Collections.Generic;
using System.Linq;
using Wasted.WastedAPI.Business_Objects;
using Xamarin.Essentials;

namespace Wasted.WastedAPI
{
    public static class DataOrganizer
    {
        /// <summary>
        /// Sorts food places by the location to the user.
        /// </summary>
        public static IEnumerable<FoodPlace> SortPlacesByUserLocation(List<FoodPlace> allFoodPlaces,
            Location userLocation,
            int nearbyPlacesCount, int maxRadiusInKilometers)
        {
            double GetDistance(Location location, FoodPlace place) =>
                Location.CalculateDistance(location, place.PlaceLocation, DistanceUnits.Kilometers);

            return (
                    from place in allFoodPlaces
                    orderby GetDistance(userLocation, place)
                    where GetDistance(userLocation, place) <= maxRadiusInKilometers
                    select place
                )
                .Take(nearbyPlacesCount);
        }

        /// <summary>
        /// Sorts deals by the percentage of change in cost. Takes the first 'offerCount' of them.
        /// </summary>
        public static IEnumerable<Deal> SortDealsByPriceChangeRatio(List<Deal> allDeals, int specialOffersCount)
        {
            return (
                    from deal in allDeals
                    orderby deal.CurrentCost / deal.PreviousCost
                    select deal
                )
                .Take(specialOffersCount);
        }

        /// <summary>
        /// Sorts food places by the number of ratings.
        /// </summary>
        public static IEnumerable<FoodPlace> SortPlacesByRatingCountDescending(List<FoodPlace> allFoodPlaces,
            int popularPlacesCount)
        {
            return (
                    from place in allFoodPlaces
                    orderby place.Ratings.Count
                    select place
                )
                .Skip(allFoodPlaces.Count - popularPlacesCount)
                .Reverse();
        }

        /// <summary>
        /// Filters some collection of deals according to 'filterCriteria'
        /// </summary>
        /// <param name="deals">Enumerable collection of deals</param>
        /// <param name="filterCriteria">Function that decides whether or not each deal should present in the filtered collection</param>
        /// <returns>Collection of deals that is filtered according to given 'filterCriteria'</returns>
        public static IEnumerable<Deal> FilterDeals(IEnumerable<Deal> deals, Func<Deal, bool> filterCriteria)
        {
            return (
                from deal in deals
                where filterCriteria(deal)
                select deal
            );
        }
    }
}
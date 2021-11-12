﻿using System;
using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Essentials;

namespace Wasted.DummyAPI
{
    public static class DataOrganizer
    {
        /// <summary>
        /// Sorts food places by the location to the user.
        /// </summary>
        public static IEnumerable<FoodPlace> SortPlacesByUserLocation(List<FoodPlace> allFoodPlaces, Location userLocation,
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
        public static IEnumerable<Deal> SortOffersByPriceChange(List<Deal> allDeals, int specialOffersCount)
        {
            return (
                   from deal in allDeals
                   orderby deal.DealCosts.PriceChange()
                   select deal
                   )
                   .Take(specialOffersCount);
        }

        /// <summary>
        /// Sorts food places by the number of ratings.
        /// </summary>
        public static IEnumerable<FoodPlace> SortPlacesByRatingDescending(List<FoodPlace> allFoodPlaces, int popularPlacesCount)
        {
            return (
                   from place in allFoodPlaces
                   orderby -place.RatingCount
                   select place
                   )
                   .Take(popularPlacesCount);
        }

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

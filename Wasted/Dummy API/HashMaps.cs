using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;

namespace Wasted.Dummy_API
{
    public static class HashMaps
    {

        // Map from food place type to food place //
        public static List<FoodPlace>[] FoodPlaceTypeHashMap { get; set; } = InitializeFoodPlaceTypeHashMap();
        public static List<FoodPlace>[] InitializeFoodPlaceTypeHashMap()
        {
            int length = DummyDataProvider.placeTypes.Length;
            List<FoodPlace>[] map = new List<FoodPlace>[length];

            for (int i = 0; i < length; ++i)
            {
                map[i] = new List<FoodPlace>();
            }
            return map;
        }

        /// <summary>
        /// Adds food places from which deals came from by using ids in a groupjoin
        /// </summary>
        public static void AddFoodPlacesToDeals(List<FoodPlace> AllFoodPlaces, List<Deal> AllDeals)
        {
            foreach (FoodPlace foodPlace in AllFoodPlaces)
            {
                var query = foodPlace.DealsIDs.
                GroupJoin(
                    AllDeals,
                    id => id,
                    deal => deal.ID,
                    (id, deal) => deal.First()
                );

                foreach (Deal deal in query)
                {
                    foodPlace.Deals.Add(deal);
                    deal.FoodPlaces.Add(foodPlace); 
                }
            }
        }
    }

}
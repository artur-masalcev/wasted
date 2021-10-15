using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.Dummy_API
{
    public static class HashMaps
    {

        // Map from food place type to food place //
        private static List<FoodPlace>[] foodPlaceTypeHashMap = InitializeFoodPlaceTypeHashMap();

        public static List<FoodPlace>[] InitializeFoodPlaceTypeHashMap()
        {
            int enumMemberCount = Enum.GetNames(typeof(PlaceType)).Length;
            List<FoodPlace>[] map = new List<FoodPlace>[enumMemberCount];

            for (int i = 0; i < enumMemberCount; ++i)
            {
                map[i] = new List<FoodPlace>();
            }
            return map;
        }

        public static List<FoodPlace>[] FoodPlaceTypeHashMap
        {
            get { return foodPlaceTypeHashMap; }
            set { foodPlaceTypeHashMap = value; }
        }

        /// <summary>
        /// Adds food places from which deals came from by using hashmaps.
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
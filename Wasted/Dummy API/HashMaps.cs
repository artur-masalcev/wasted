using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.Dummy_API
{
    public static class HashMaps
    {
        // Map from deal id to deal //
        public static Dictionary<int, Deal> dealsHashMap = new Dictionary<int, Deal>();
        public static Dictionary<int, Deal> DealsHashMap
        {
            get { return dealsHashMap; }
            set { dealsHashMap = value; }
        }

        // Map from food place id to food place //
        private static Dictionary<int, FoodPlace> foodPlacesHashMap = new Dictionary<int, FoodPlace>();
        public static Dictionary<int, FoodPlace> FoodPlacesHashMap
        {
            get { return foodPlacesHashMap; }
            set { foodPlacesHashMap = value; }
        }

        /// <summary>
        /// Adds restaurants from which deals came from by using hashmaps.
        /// !!! Deals must be created before for this function to work
        /// </summary>
        public static void AddRestaurantsToDeals(List<FoodPlace> AllFoodPlaces)
        {
            foreach (FoodPlace foodPlace in AllFoodPlaces)
            {
                foreach (int dealId in foodPlace.DealsIDs)
                {
                    Deal deal = DealsHashMap[dealId];
                    deal.FoodPlaces.Add(foodPlace);

                    foodPlace.Deals.Add(deal);
                }
            }
        }
    }

}
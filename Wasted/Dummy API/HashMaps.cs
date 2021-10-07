using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.Dummy_API
{
    public static class HashMaps
    {
        // Map from deal id to deal //
        public static Dictionary<int, Deal> DealsHashMap { get; set; } = new Dictionary<int, Deal>();

        // Map from food place id to food place //
        public static Dictionary<int, FoodPlace> FoodPlacesHashMap { get; set; } = new Dictionary<int, FoodPlace>();
  

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
        /// !!! Deals must be created before for this function to work
        /// </summary>
        public static void AddFoodPlacesToDeals(List<FoodPlace> AllFoodPlaces)
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
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

    }

}

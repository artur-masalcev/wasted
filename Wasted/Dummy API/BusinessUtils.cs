using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API
{
    public static class BusinessUtils
    {
        /// <summary>
        /// Dictionary from place type to places of that type
        /// </summary>
        public static Dictionary<string, List<FoodPlace>> PlaceTypeDictionary { get; set; } 
            = new Dictionary<string, List<FoodPlace>>();

        /// <summary>
        /// Adds food places from which deals came from by using ids
        /// </summary>
        public static void AddFoodPlacesToDeals()
        {
            DataService service = DependencyService.Get<DataService>();
            foreach (FoodPlace foodPlace in service.AllFoodPlaces)
            {
                foreach (int dealID in foodPlace.DealsIDs)
                {
                    foodPlace.Deals.Add(service.AllDeals[dealID - 1]);
                    service.AllDeals[dealID - 1].FoodPlaces.Add(foodPlace);
                }
            }
        }

        /// <summary>
        /// Sorts array by id
        /// </summary>
        public static List<T> CountingSort<T>(IIntegerIdentifiable[] array)
        {
            List<T> newArray = new List<T>(new T[array.Length]);
            foreach (IIntegerIdentifiable elem in array)
            {
                newArray[elem.ID - 1] = (T)elem;
            }
            return newArray;
        }
    }

}
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
    public static class HashMaps
    {
        /// <summary>
        /// Dictionary from place type to places of that type
        /// </summary>
        public static Dictionary<string, List<FoodPlace>> PlaceTypeDictionary { get; set; } = new Dictionary<string, List<FoodPlace>>();
            

        /// <summary>
        /// Adds food places from which deals came from by using ids in a groupjoin
        /// </summary>
        public static void AddFoodPlacesToDeals()
        {
            DataService service = DependencyService.Get<DataService>();
            foreach (FoodPlace foodPlace in service.AllFoodPlaces)
            {
                var query = foodPlace.DealsIDs.
                GroupJoin(
                    service.AllDeals,
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
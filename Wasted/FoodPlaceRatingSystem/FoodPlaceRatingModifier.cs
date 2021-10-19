using System;
using System.Collections.Generic;
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.FoodPlaceRatingSystem
{
    /// <summary>
    /// Contains tools for modifying rating data
    /// </summary>
    public class FoodPlaceRatingModifier
    {

        /// <summary>
        /// Adds or changes user's rating for the food place
        /// </summary>
        /// <param name="UserID">Unique user ID. Required if user wishes to change his current rate, also prevents data duplicity</param>
        /// <param name="FoodPlace">Food place to be rated</param>
        /// <param name="NewRating">Rating for the food place</param>
        public static void SetUserVote(int userID, FoodPlace selectedFoodPlace, int newRating)
        {

        }
    }
}

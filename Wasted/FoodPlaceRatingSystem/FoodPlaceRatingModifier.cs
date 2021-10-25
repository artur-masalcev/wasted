using System;
using System.Collections.Generic;
using Wasted.Dummy_API.Business_Objects;
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
        /// 

        public static void SetUserVote(User user, FoodPlace selectedFoodPlace, int newRating)
        {
            int key = selectedFoodPlace.ID;

            if (user.Ratings.ContainsKey(key))
                ResetRating(selectedFoodPlace, user.Ratings[key]);

            selectedFoodPlace.Rating = newRating;
            user.Ratings[key] = newRating;
        }
    }
}

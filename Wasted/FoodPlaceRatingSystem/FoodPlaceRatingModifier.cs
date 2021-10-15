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
        /// <param name="FoodPlaceID">ID of the food place</param>
        /// <param name="NewRating">Rating for the food place</param>
        /// 

        private static void ResetRating(FoodPlace SelectedFoodPlace, int previousRating)
        {
            --SelectedFoodPlace.RatingCount;
            double previousRestaurantRating = (SelectedFoodPlace.Rating * (double)(SelectedFoodPlace.RatingCount + 1) - previousRating) / SelectedFoodPlace.RatingCount;
            double tempRating = previousRestaurantRating * (SelectedFoodPlace.RatingCount + 1) - SelectedFoodPlace.Rating * SelectedFoodPlace.RatingCount;

            SelectedFoodPlace.Rating = tempRating;
            --SelectedFoodPlace.RatingCount;
        }

        public static void SetUserVote(int userID, FoodPlace selectedFoodPlace, int newRating)
        {
            int key = selectedFoodPlace.ID;
            Dictionary<int, int> userRatings = App.Ratings[userID];

            if (userRatings.ContainsKey(key))
                ResetRating(selectedFoodPlace, userRatings[key]);

            selectedFoodPlace.Rating = newRating;
            userRatings[key] = newRating;
        }
    }
}

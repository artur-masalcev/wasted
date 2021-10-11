using System;
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
            SelectedFoodPlace.Rating =
                (SelectedFoodPlace.Rating * (SelectedFoodPlace.RatingCount + 1) - previousRating) / SelectedFoodPlace.RatingCount * (SelectedFoodPlace.RatingCount + 1)
                - SelectedFoodPlace.Rating * SelectedFoodPlace.RatingCount;
            --SelectedFoodPlace.RatingCount;
        }

        public static void SetUserVote(int userID, FoodPlace selectedFoodPlace, int newRating)
        {
            selectedFoodPlace.Rating = newRating;
            int key = selectedFoodPlace.ID;

            if (App.ratings.ContainsKey(key))
                ResetRating(selectedFoodPlace, App.ratings[key]);

            selectedFoodPlace.Rating = newRating;
            App.ratings[key] = newRating; //TODO: save ratings to file after exiting the app.

        }
    }
}

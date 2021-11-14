using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;

namespace Wasted.FoodPlaceRatingSystem
{
    /// <summary>
    /// Contains tools for modifying rating data
    /// </summary>
    public class FoodPlaceRatingModifier
    {
        /// <summary>
        /// Allows user to rethink their rating choice.
        /// </summary>
        private static void ResetRating(FoodPlace SelectedFoodPlace, int previousRating)
        {
            --SelectedFoodPlace.RatingCount;
            double previousRestaurantRating = (SelectedFoodPlace.Rating * (SelectedFoodPlace.RatingCount + 1) - previousRating) / SelectedFoodPlace.RatingCount;
            double tempRating = previousRestaurantRating * (SelectedFoodPlace.RatingCount + 1) - SelectedFoodPlace.Rating * SelectedFoodPlace.RatingCount;

            SelectedFoodPlace.Rating = tempRating;
            --SelectedFoodPlace.RatingCount;
        }

        /// <summary>
        /// Adds or changes user's rating for the food place
        /// </summary>
        /// <param name="User">Unique user. Required if user wishes to change his current rate, also prevents data duplicity</param>
        /// <param name="FoodPlace">Food place</param>
        /// <param name="NewRating">Rating for the food place</param>
        /// 

        public static void SetUserVote(User user, FoodPlace selectedFoodPlace, int newRating)
        {
            int key = selectedFoodPlace.ID;

            if (user.Ratings.ContainsKey(key))
                ResetRating(selectedFoodPlace, user.Ratings[key]); //Rethinking rating choice

            selectedFoodPlace.Rating = newRating;
            user.Ratings[key] = newRating;
            DataProvider.WriteAllUserClients();
        }
    }
}

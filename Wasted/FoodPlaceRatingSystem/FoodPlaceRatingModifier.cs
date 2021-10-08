using System;
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
        public static void SetUserVote(int UserID, int FoodPlaceID, int NewRating)
        {
            //TODO: implement rating modification
        }
    }
}

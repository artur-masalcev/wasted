using System;
namespace Wasted.FoodPlaceRatingSystem
{
    /// <summary>
    /// Includes functions for representing numeric values of rating in user-friendly way
    /// </summary>
    public class RatingToAssociation
    {
        /// <summary>
        /// Converts rating numeric value to emoji. Rating should be between 1 and 5
        /// If rating value is less than 1 it is interpreted as 1
        /// If rating value is greater than 5 it is interpreted as 5
        /// </summary>
        /// <param name="rating">Value of rating</param>
        /// <returns>Emoji representation for provided 'rating'</returns>
        public static String ConvertToEmoji(int rating)
        {
            if (rating <= 1) return "🤢";
            else if (rating == 2) return "😡";
            else if (rating == 3) return "😐";
            else if (rating == 4) return "😋";
            else if (rating >= 5) return "😍";

            return "";
        }

        /// <summary>
        /// Converts rating numeric value to associative comment. Rating should be between 1 and 5
        /// If rating value is less than 1 it is interpreted as 1
        /// If rating value is greater than 5 it is interpreted as 5
        /// </summary>
        /// <param name="rating">Value of rating</param>
        /// <returns>Associative comment for provided 'rating'</returns>
        public static String ConvertToComment(int rating)
        {
            if (rating <= 1) return "Disgusting";
            else if (rating == 2) return "Ew";
            else if (rating == 3) return "Not bad";
            else if (rating == 4) return "Great";
            else if (rating >= 5) return "Amazing";

            return "";
        }
    }
}

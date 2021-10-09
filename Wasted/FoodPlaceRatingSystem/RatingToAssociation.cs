using System;
namespace Wasted.FoodPlaceRatingSystem
{
    //TODO: as several languages might be supported in future, all functions
    //should take some arrays of objects(probably strings) and return elements from them instead of predefined values

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
            switch (rating)
            {
                case 0: return "🤬";
                case 1: return "🤢";
                case 2: return "😡";
                case 3: return "😐";
                case 4: return "😋";
                case 5: return "😍";
                default: throw new ArgumentOutOfRangeException();
            }
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
            switch (rating)
            {
                case 0: return "#@$?!";
                case 1: return "Disgusting";
                case 2: return "Ew";
                case 3: return "Not bad";
                case 4: return "Great";
                case 5: return "Amazing";
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}

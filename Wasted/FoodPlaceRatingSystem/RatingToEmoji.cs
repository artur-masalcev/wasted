using System;
namespace Wasted.FoodPlaceRatingSystem
{
    //TODO: as several languages might be supported in future, all functions
    //should take some arrays of objects(probably strings) and return elements from them instead of predefined values

    /// <summary>
    /// Includes functions for representing numeric values of rating in user-friendly way
    /// </summary>

    public class Emojis
    {
        static string[] emojis = new string[]
        {
            "🤬", "🤢", "😡", "😐", "😋", "😍"
        };

        public string this[int index] =>
            index >= 0 && index < emojis.Length ? emojis[index] : throw new ArgumentOutOfRangeException();
    }

    public class Comments
    {
        static string[] comments = new string[]
        {
            "#@$?!", "Disgusting", "Ew", "Not bad", "Great", "Amazing"
        };

        public string this[int index] =>
            index >= 0 && index < comments.Length ? comments[index] : throw new ArgumentOutOfRangeException();
    }

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
            return new Emojis()[rating];
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
            return new Comments()[rating];
        }
    }
}

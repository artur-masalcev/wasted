using System;

namespace Wasted.Pages.Client.FoodPlaceRating
{
    //TODO: as several languages might be supported in future, all functions
    //should take some arrays of objects(probably strings) and return elements from them instead of predefined values

    /// <summary>
    /// Includes functions for representing numeric values of rating in user-friendly way
    /// </summary>
    public class Emojis
    {
        static readonly string[] _emojis =
        {
            "🤬", "🤢", "😡", "😐", "😋", "😍"
        };

        public string this[int index] =>
            index >= 0 && index < _emojis.Length ? _emojis[index] : throw new ArgumentOutOfRangeException();
    }

    public class Comments
    {
        static readonly string[] _comments =
        {
            "#@$?!", "Disgusting", "Ew", "Not bad", "Great", "Amazing"
        };

        public string this[int index] =>
            index >= 0 && index < _comments.Length ? _comments[index] : throw new ArgumentOutOfRangeException();
    }

    public static class RatingToAssociation
    {
        /// <summary>
        /// Converts rating numeric value to emoji. Rating should be between 1 and 5
        /// </summary>
        /// <param name="rating">Value of rating</param>
        /// <returns>Emoji representation for provided 'rating'</returns>
        public static String ConvertToEmoji(int rating)
        {
            return new Emojis()[rating];
        }

        /// <summary>
        /// Converts rating numeric value to associative comment. Rating should be between 1 and 5
        /// </summary>
        /// <param name="rating">Value of rating</param>
        /// <returns>Associative comment for provided 'rating'</returns>
        public static String ConvertToComment(int rating)
        {
            return new Comments()[rating];
        }
    }
}
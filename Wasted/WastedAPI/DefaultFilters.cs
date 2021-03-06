using Wasted.WastedAPI.Business_Objects;

namespace Wasted.WastedAPI
{
    /// <summary>
    /// Contains reusable functions for filtering collections
    /// </summary>
    public static class DefaultFilters
    {
        /// <summary>
        /// Determines whether or not deal is in stock
        /// </summary>
        /// <param name="deal">The given deal</param>
        /// <returns>Returns true if quantity of given 'deal' is more than zero. Returns false otherwise</returns>
        public static bool DealInStock(Deal deal)
        {
            return deal.Quantity > 0;
        }
    }
}
using NUnit.Framework;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;

namespace WastedTest
{
    public class DefaultFiltersTest
    {
        [Test]
        public void ShouldReturnTrue_WhenPositiveDealQuantity()
        {
            Deal deal = new Deal(quantity: 10);
            Assert.True(DefaultFilters.DealInStock(deal));
        }
        
        [Test]
        public void ShouldReturnFalse_WhenNoDeals()
        {
            Deal deal = new Deal(quantity: 0);
            Assert.False(DefaultFilters.DealInStock(deal));
        }
    }
}
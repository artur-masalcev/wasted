using NUnit.Framework;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;

namespace Wasted.UnitTests
{
    public class DefaultFiltersTest
    {
        [Test]
        public void DealInStock_ShouldReturnTrue_WhenPositiveDealQuantity()
        {
            // Arrange
            var deal = new Deal(quantity: 10);

            // Act
            var result = DefaultFilters.DealInStock(deal);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void DealInStock_ShouldReturnFalse_WhenNoDeals()
        {
            // Arrange
            var deal = new Deal(quantity: 0);

            // Act
            var result = DefaultFilters.DealInStock(deal);

            // Assert
            Assert.False(result);
        }
    }
}
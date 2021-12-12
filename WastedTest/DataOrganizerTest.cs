using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;

namespace WastedTest
{
    public class DataOrganizerTest
    {
        private readonly List<Deal> _dealsList = new List<Deal>
        {
            new Deal(quantity:10),
            new Deal(quantity:5),
            new Deal(quantity:3)
        };
            
        [Test]
        public void ShouldKeepDeals_WhenPassCriteria()
        {
            IEnumerable<Deal> filteredDeals = DataOrganizer.FilterDeals(
                _dealsList, deal => deal.Quantity > 0);
            Assert.AreEqual(3, filteredDeals.Count());
        }
        
        [Test]
        public void ShouldFilterOutDeals_WhenNotPassCriteria()
        {
            IEnumerable<Deal> filteredDeals = DataOrganizer.FilterDeals(
                _dealsList, deal => deal.Quantity > 5);
            Assert.AreEqual(1, filteredDeals.Count());
        }
    }
}
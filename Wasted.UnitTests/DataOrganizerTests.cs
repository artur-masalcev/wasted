using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;

namespace Wasted.UnitTests
 {
     public class DataOrganizerTests
     {

         /// <summary>
         /// Collection of 'Deal's with different value of 'quantity'
         /// </summary>
         private readonly List<Deal> dealsWithDifferentQuantity = new List<Deal>
         {
             new Deal(quantity:10),
             new Deal(quantity:5),
             new Deal(quantity:3)
         };

         /// <summary>
         /// Unsorted collection of 'Deal's with different price change
         /// </summary>
         private readonly List<Deal> dealsWithDifferentPriceChangeUnsorted = new List<Deal>
         {
             new Deal(id: 1, currentCost: 1, previousCost: 100),
             new Deal(id: 2, currentCost: 1, previousCost: 10),
             new Deal(id: 3, currentCost: 1, previousCost: 10000),
             new Deal(id: 4, currentCost: 1, previousCost: 1000),
             new Deal(id: 5, currentCost: 1, previousCost: 100000)
         };

         private readonly List<Deal> dealsWithDifferentPriceChangeSortedByPriceChangeRatioDesc = new List<Deal>
         {
             new Deal(id: 5, currentCost: 1, previousCost: 100000),
             new Deal(id: 3, currentCost: 1, previousCost: 10000),
             new Deal(id: 4, currentCost: 1, previousCost: 1000),
             new Deal(id: 1, currentCost: 1, previousCost: 100),
             new Deal(id: 2, currentCost: 1, previousCost: 10)
         };

         [Test]
         public void FilterDeals_ShouldKeepDeals_WhenPassCriteria()
         {
             // Arrange
             var testedDeals = dealsWithDifferentQuantity;

             // Act
             IEnumerable<Deal> filteredDeals = DataOrganizer.FilterDeals(
                 testedDeals, deal => deal.Quantity > 0);

             // Assert
             Assert.AreEqual(3, filteredDeals.Count());
         }

         [Test]
         public void FilterDeals_ShouldFilterOutDeals_WhenNotPassCriteria()
         {
             // Arrange
             var testedDeals = dealsWithDifferentQuantity;

             // Act
             var filteredDeals
                 = DataOrganizer.FilterDeals(testedDeals, deal => deal.Quantity > 5);

             // Assert
             Assert.AreEqual(1, filteredDeals.Count());
         }

         [Test]
         public void SortDealsByPriceChangeRatio_ShouldSortInDescendingOrderByPriceChangeRatio()
         {
             // Arrange
             var testedDeals = dealsWithDifferentPriceChangeUnsorted;
             var numberOfDealsToKeep = testedDeals.Count;
             var expectedResult = dealsWithDifferentPriceChangeSortedByPriceChangeRatioDesc;

             // Act
             var potentiallySortedDeals 
                 = DataOrganizer.SortDealsByPriceChangeRatio(testedDeals, numberOfDealsToKeep).ToList();
             var isResultValid = potentiallySortedDeals.SequenceEqual(expectedResult);

             // Assert
             Assert.IsTrue(isResultValid);
         }
     }
 } 
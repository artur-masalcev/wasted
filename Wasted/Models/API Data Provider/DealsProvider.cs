using System.Collections.Generic;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;

namespace Wasted.WastedAPI
{
    public static class DealsProvider
    {
        private const string DealsRoute = "deals";
        
        public static Deal GetDealById(int id)
        {
            return DataFetchingUtils.GetBusinessObject<Deal>(DealsRoute, id);
        }

        public static List<Deal> GetBestOffers(int specialOffersCount)
        {
            return DataFetchingUtils.GetBusinessObject<List<Deal>>(DealsRoute, "special", specialOffersCount);
        }
        
        public static void CreateDeal(Deal deal)
        {
            DataFetchingUtils.CreateBusinessObject(deal, DealsRoute);
        }
        
        public static void UpdateDeal(Deal deal)
        {
            DataFetchingUtils.UpdateBusinessObject(deal, DealsRoute);
        }

        public static void DeleteDeal(Deal deal)
        {
            DataFetchingUtils.DeleteBusinessObject(DealsRoute, deal.Id);
        }
    }
}
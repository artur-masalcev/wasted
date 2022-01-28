using System.Collections.Generic;
using Wasted.Utils;
using Wasted.WastedAPI.Business_Objects;

namespace Wasted.WastedAPI
{
    public static class OrdersProvider
    {
        private const string OrdersRoute = "orders";

        public static List<OrderDeal> GetClientOrdersByIdAndStatus(int clientUserId, string status) //TODO: think about naming
        {
            return DataFetchingUtils.GetBusinessObject<List<OrderDeal>>(OrdersRoute,
                UsersProvider.ClientUsersRoute, clientUserId, status);

        }

        public static IEnumerable<OrderDeal> GetClientOrdersByIdAndNotStatus(int clientUserId, string status)
        {
            return DataFetchingUtils.GetBusinessObject<List<OrderDeal>>(OrdersRoute,
                UsersProvider.ClientUsersRoute, clientUserId, "not", status);
            
        }

        public static List<OrderDeal> GetPlaceOrdersById(int placeUserId)
        {
            return DataFetchingUtils.GetBusinessObject<List<OrderDeal>>(OrdersRoute,
                UsersProvider.PlaceUsersRoute, placeUserId);
        }

        public static void CreateOrder(OrderDeal orderDeal)
        {
            DataFetchingUtils.CreateBusinessObject(orderDeal, OrdersRoute);
        }

        public static void UpdateOrders(List<OrderDeal> orderDeals)
        {
            DataFetchingUtils.UpdateBusinessObject(orderDeals, OrdersRoute);
        }

        public static void UpdateOrder(OrderDeal orderDeal)
        {
            DataFetchingUtils.UpdateBusinessObject(new List<OrderDeal>{orderDeal}, OrdersRoute);
        }
    }
}
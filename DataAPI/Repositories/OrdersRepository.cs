using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{
    public class OrdersRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public OrdersRepository(AppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<Order> GetClientOrdersByIdAndStatus(int clientUserId, string status)
        {
            return Get().Where(order => order.ClientUserId == clientUserId)
                .Where(order => order.Status == status);
        }
        
        public IEnumerable<Order> GetClientOrdersByIdAndNotStatus(int clientUserId, string status)
        {
            return Get().Where(order => order.ClientUserId == clientUserId)
                .Where(order => order.Status != status);
        }
        
        public IEnumerable<Order> GetPlaceOrdersById(int placeUserId)
        {
            return Get().Where(order => order.Deal.FoodPlace.PlaceUserId == placeUserId); //TODO: think if not too much fetching in get() method
        }

        public Order Create(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChangesAsync();
            return order;
        }

        public void UpdateOrders(List<Order> orders)
        {
            orders.ForEach(UpdateOrder);
            _dbContext.SaveChangesAsync();
        }

        private void UpdateOrder(Order order)
        {
            Order oldOrder = _dbContext.Orders.Find(order.Id); //TODO: do deep copy automatically
            oldOrder.Status = order.Status;
            oldOrder.ExpectedFinishTime = order.ExpectedFinishTime;
            try
            {
                _logger.Information(
                    "Order's \"{OrderTitle}\" (Order id: {OrderId}) has changed to {OrderStatus}.\n",
                    order.Deal.Title, order.Id, order.Status);
            }
            catch
            {
                _logger.Information(
                    "Order's status with id {OrderId} has changed to {OrderStatus}.\n",
                    order.Id, order.Status);
            }
        }

        private IEnumerable<Order> Get()
        {
            return _dbContext.Orders
                .Include(o => o.Deal)
                .ThenInclude(d => d.FoodPlace)
                .ThenInclude(f => f.PlaceUser);
        }
    }
}
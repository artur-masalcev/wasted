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

        public IEnumerable<Order> Get()
        {
            return _dbContext.Orders
                .Include(o => o.Deal)
                .ThenInclude(d => d.FoodPlace)
                .ThenInclude(f => f.PlaceUser)
                .ToList();
        }

        public Order Create(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChangesAsync();
            return order;
        }

        public void Update(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                Order oldOrder = _dbContext.Orders.Find(order.Id);
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
            _dbContext.SaveChangesAsync();
        }
    }
}
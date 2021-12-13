using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAPI.Repositories
{
    public class OrdersRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<OrdersRepository> _logger;

        public OrdersRepository(AppDbContext dbContext, ILogger<OrdersRepository> logger)
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
                _logger.LogError("asdfa");
                // oldOrder.Status = order.Status;
                // oldOrder.ExpectedFinishTime = order.ExpectedFinishTime;
                int i = 2;
            }
            _dbContext.SaveChangesAsync();
        }
    }
}
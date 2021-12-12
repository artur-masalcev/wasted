using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class OrdersRepository
    {
        private readonly AppDbContext _dbContext;

        public OrdersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> Get()
        {
            return _dbContext.Orders
                .Include(o => o.Deal)
                .ThenInclude(d => d.FoodPlace)
                .ThenInclude(f => f.PlaceUser)
                .ToList(); //TODO: think if to list is needed
        }

        public Order Create(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
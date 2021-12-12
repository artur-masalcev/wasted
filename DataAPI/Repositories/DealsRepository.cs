using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public interface IDealsRepository
    {
        IEnumerable<Deal> Get();
        Deal Create(Deal deal);
        void Update(Deal deal);
    }

    public class DealsRepository : IDealsRepository
    {
        private readonly AppDbContext _dbContext;

        public DealsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Deal> Get()
        {
            return _dbContext.Deals
                .Include(d => d.FoodPlace)
                .ToList();
        }

        public Deal Create(Deal deal)
        {
            _dbContext.Deals.Add(deal);
            _dbContext.SaveChangesAsync();
            return deal;
        }

        public void Update(Deal deal)
        {
            Deal oldDeal = _dbContext.Deals.Find(deal.Id);
            oldDeal.Quantity = deal.Quantity;
            _dbContext.SaveChangesAsync();
        }
    }
}
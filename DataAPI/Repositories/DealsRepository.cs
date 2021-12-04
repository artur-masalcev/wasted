using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class DealsRepository
    {
        private readonly AppDbContext _dbContext;
        public DealsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Deal> Get()
        {
            return  _dbContext.Deals
                .Include(d => d.FoodPlace)
                .ToList();
        }
        
        public Deal Create(Deal deal)   
        {
            _dbContext.Deals.Add(deal);
            _dbContext.SaveChangesAsync();
            return deal;
        }
    }
}
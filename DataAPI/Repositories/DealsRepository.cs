using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class DealsRepository
    {
        private readonly AppContext _context;
        public DealsRepository(AppContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Deal> Get()
        {
            return  _context.Deals
                .Include(d => d.FoodPlace)
                .ToList();
        }
        
        public Deal Create(Deal deal)   
        {
            _context.Deals.Add(deal);
            _context.SaveChangesAsync();
            return deal;
        }
    }
}
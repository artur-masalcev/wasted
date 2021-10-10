using DealAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealAPI.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly DealContext _context;

        public DealRepository(DealContext context)
        {
            _context = context;
        }

        public async Task<Deal> Create(Deal deal)
        {
            _context.deals.Add(deal);
            await _context.SaveChangesAsync();

            return deal;
        }

        public async Task Delete(int id)
        {
            var dealToDelete = await _context.deals.FindAsync(id);
            _context.deals.Remove(dealToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Deal>> Get()
        {
            return await _context.deals.ToListAsync();
        }

        public async Task<Deal> Get(int id)
        {
            return await _context.deals.FindAsync(id);
        }

        public async Task Update(Deal deal)
        {
            _context.Entry(deal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

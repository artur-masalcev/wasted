using System.Collections.Generic;
using System.Threading.Tasks;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class StoreRepository
    {
        private readonly BookContext _context;
        public StoreRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> Get()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> Create(Store book)
        {
            _context.Stores.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
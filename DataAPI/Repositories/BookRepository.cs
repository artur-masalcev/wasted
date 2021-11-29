using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class BookRepository
    {
        private readonly BookContext _context;
        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public  IEnumerable<Book> Get()
        {
            return  _context.Books
                .Include(r => r.Store).ToList();
        }

        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
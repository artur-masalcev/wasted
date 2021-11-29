using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Models
{
    public class BookContext : DbContext
    {
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Store> Stores { get; set; }
        
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
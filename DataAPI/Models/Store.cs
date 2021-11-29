using System.Collections.Generic;

namespace DataAPI.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        
        public List<Book> Books { get; set; }
    }
}
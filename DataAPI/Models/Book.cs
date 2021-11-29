using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int StoreId { get; set; }
        public Store Store { get; set; } //blog (class post)
    }
}
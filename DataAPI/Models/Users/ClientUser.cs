using System.Collections.Generic;

namespace DataAPI.Models
{
    public class ClientUser : AbstractUser
    {
        public int Id { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
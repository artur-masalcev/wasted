using System.Collections.Generic;

namespace DataAPI.Models
{
    public class ClientUser : AbstractUser
    {
        public List<Rating> Ratings { get; set; }
    }
}
using System.Collections.Generic;

namespace DataAPI.Models.Users
{
    public class ClientUser : AbstractUser
    {
        public List<Rating> Ratings { get; set; }
    }
}
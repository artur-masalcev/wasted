using System.Collections.Generic;
using Wasted;

namespace DataAPI.Models.Users
{
    public class ClientUser : AbstractUser
    {
        public override string UserType => Constants.ClientType;
        public List<Rating> Ratings { get; set; }
    }
}
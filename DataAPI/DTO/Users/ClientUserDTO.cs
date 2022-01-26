using System.Collections.Generic;
using DataAPI.Models.Users;
using Wasted;

namespace DataAPI.DTO
{
    public class ClientUserDTO : AbstractUser
    {
        public override string UserType => Constants.ClientType;
        public List<RatingDTO> Ratings { get; set; }
    }
}
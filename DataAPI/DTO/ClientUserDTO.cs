using System.Collections.Generic;
using DataAPI.Models.Users;

namespace DataAPI.DTO
{
    public class ClientUserDTO : AbstractUser
    {
        public List<RatingDTO> Ratings { get; set; }
    }
}
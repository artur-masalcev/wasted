using System.Collections.Generic;
using DataAPI.Models;

namespace DataAPI.DTO
{
    public class ClientUserDTO : AbstractUser
    {
        public List<RatingDTO> Ratings { get; set; }
    }
}
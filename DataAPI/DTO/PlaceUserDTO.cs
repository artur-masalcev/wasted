using System.Collections.Generic;
using DataAPI.Models.Users;

namespace DataAPI.DTO
{
    public class PlaceUserDTO : AbstractUser
    {
        public List<FoodPlaceDTO> FoodPlaces { get; set; }
    }
}
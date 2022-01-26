using System.Collections.Generic;
using DataAPI.Models.Users;
using Wasted;

namespace DataAPI.DTO
{
    public class PlaceUserDTO : AbstractUser
    {
        public override string UserType => Constants.PlaceType;
        public List<FoodPlaceDTO> FoodPlaces { get; set; }
    }
}
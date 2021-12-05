﻿using System.Collections.Generic;
using DataAPI.Models;

namespace DataAPI.DTO
{
    public class PlaceUserDTO : AbstractUser
    {
        public List<FoodPlaceDTO> FoodPlaces { get; set; }
    }
}
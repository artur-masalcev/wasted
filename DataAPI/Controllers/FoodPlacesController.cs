﻿using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodPlacesController : ControllerBase
    {   

        [HttpGet]
        public string Get()
        {
            return DummyDataProvider.GetText(DummyDataProvider.FoodPlacesJSONPath);
        }

        [HttpPost("add")]
        public void Create([FromBody] string AllFoodPlaces)
        {
            DummyDataProvider.WriteText(DummyDataProvider.FoodPlacesJSONPath, AllFoodPlaces);
        }
    }
}

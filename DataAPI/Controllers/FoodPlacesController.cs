using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public IEnumerable<FoodPlace> Get()
        {
            return DummyDataProvider.GetFoodPlaces();
        }
    }
}

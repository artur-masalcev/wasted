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
    public class RatingsController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return DummyDataProvider.GetText(DummyDataProvider.RatingsJSONPath);
        }

        [HttpPost("add")]
        public void Create([FromBody] string Ratings)
        {
            DummyDataProvider.WriteText(DummyDataProvider.RatingsJSONPath, Ratings);
        }
    }
}

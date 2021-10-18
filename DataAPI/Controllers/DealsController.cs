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
    public class DealsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Deal> Get()
        {
            return DummyDataProvider.GetDeals();
        }

        [HttpPost("add")]
        public void Create([FromBody] string AllDeals)
        {
            DummyDataProvider.WriteDeals(AllDeals);
        }
    }
}
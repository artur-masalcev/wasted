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
        public string Get()
        {
            return DummyDataProvider.GetText(DummyDataProvider.DealsJSONPath);
        }

        [HttpPost("add")]
        public void Create([FromBody] string AllDeals)
        {
            DummyDataProvider.WriteText(DummyDataProvider.DealsJSONPath, AllDeals);
        }
    }
}
using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealsController : DataController
    {
        public override string GetPathToData()
        {
            return DummyDataProvider.DealsJSONPath;
        }
    }
}
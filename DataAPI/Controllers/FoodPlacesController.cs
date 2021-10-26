using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodPlacesController : DataController
    {
        public override string GetPathToData()
        {
            return DummyDataProvider.FoodPlacesJSONPath;
        }
    }
}

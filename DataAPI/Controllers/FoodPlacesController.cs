using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;

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

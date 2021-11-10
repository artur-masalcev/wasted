using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    public abstract class DataController : ControllerBase
    {
        public abstract string GetPathToData();
        [HttpGet]
        public string Get()
        {
            return DummyDataProvider.GetText(GetPathToData());
        }

        [HttpPost("add")]
        public void Create([FromBody] string data)
        {
            DummyDataProvider.WriteText(GetPathToData(), data);
        }
    }
}
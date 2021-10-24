using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;


namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return DummyDataProvider.GetText(DummyDataProvider.UsersJSONPath);
        }

        [HttpPost("add")]
        public void Create([FromBody] string AllUsers)
        {
            DummyDataProvider.WriteText(DummyDataProvider.UsersJSONPath, AllUsers);
        }
    }
}
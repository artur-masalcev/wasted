using DataAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientUsersController : DataController
    {
        public override string GetPathToData()
        {
            return DummyDataProvider.ClientUsersJSONPath;
        }
    }
}
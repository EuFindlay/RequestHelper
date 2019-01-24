using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelperTestApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("/GetValue")]
        public ActionResult<string> Get([FromQuery]TestUrlModel model)
        {
            return "Test";
        }
    }
}

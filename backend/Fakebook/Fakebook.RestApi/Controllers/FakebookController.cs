using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakebookController : ControllerBase
    {
        // GET: api/<Fakebook>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");

        }
    }
}

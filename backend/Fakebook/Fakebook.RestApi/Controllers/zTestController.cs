using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class zTestController
    {
        [HttpGet]
        public string Get()
        {
            return "Success";
        }
    }
}

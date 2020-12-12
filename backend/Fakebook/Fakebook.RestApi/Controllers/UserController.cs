using Fakebook.DataAccess.Model.Repository;
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
    // /api/user
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;

        public UserController(IUserRepo repository)
        {
            _repo = repository;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult Get()
        {
            var users = _repo.GetAllUsers();
            return Ok(users);

        }
    }
}

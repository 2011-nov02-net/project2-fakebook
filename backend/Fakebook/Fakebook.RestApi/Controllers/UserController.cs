using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns></returns>
        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repo.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.GetUserById(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (await _repo.CreateUser(user))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// A delete method that wlil return False if it can't find that id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repo.DeleteUser(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}/")]
        public async Task<IActionResult> Put(int id, User user)
        {
            // if the id is null switch to bad request
            if ( id == 0 || user.IsValid())
            {
                await _repo.UpdateUser(id, user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /*
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _repo.GetUserByEmail(email);
            return Ok(user);
        }
        */
    }
}

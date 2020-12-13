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
        [HttpPost("{id}/{post}/")]
        public async Task<IActionResult> Post(int id, string post)
        {
            var user = await _repo.GetUserById(id);

            string message = user.FirstName + "says we need to create a function that can create posts" + post;
            return Ok(message);
        }
        /// <summary>
        /// A delete method that wlil return False if it can't find that id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(await _repo.DeleteUser(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User user)
        {
            if (await _repo.UpdateUser(user))
            {
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

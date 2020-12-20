using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public FollowController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        // POST: api/User/{id}/follow/{userId}
        [HttpPost("{id}/follow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Follow(int id, int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email == user.Email)
            {
                if (await _userRepo.FollowUserAsync(id, userId))
                {
                    return Ok("Success");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }

        [HttpPost("/follow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Follow(int userId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user != null)
            {
                if (await _userRepo.FollowUserAsync(user.Id, userId))
                {
                    return Ok("Success");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }

        // POST: api/Posts/{id}/unlike/{userId}
        [HttpPost("{id}/unfollow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Unfollow(int id, int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email == user.Email)
            {
                if (await _userRepo.UnfollowUserAsync(id, userId))
                {
                    return Ok("Success");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }

        [HttpPost("/follow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Unfollow(int userId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user != null)
            {
                if (await _userRepo.UnfollowUserAsync(user.Id, userId))
                {
                    return Ok("Success");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}

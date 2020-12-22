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

        // POST: api/User/{id}/follow/
        [HttpPost("{id}/follow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Follow(int id, int userId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (email.ToLower() == user.Email.ToLower())
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

        // POST: api/User/{id}/unfollow/
        [HttpPost("{id}/unfollow/{userId}")]
        [Authorize]
        public async Task<IActionResult> Unfollow(int id, int userId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (email.ToLower() == user.Email.ToLower())
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
    }
}

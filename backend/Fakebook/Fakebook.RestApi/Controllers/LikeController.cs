using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;

        public LikeController(IPostRepo postRepo, IUserRepo userRepo) {
            _postRepo = postRepo;
            _userRepo = userRepo;
        }

        // POST: api/Posts/{id}/like/{userId}
        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<IActionResult> Like(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await  _userRepo.GetUserByEmailAsync(email);
            if (email == user.Email) {
                if (await _postRepo.LikePostAsync(id, user.Id))
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
        [HttpPost("{id}/unlike")]
        [Authorize]
        public async Task<IActionResult> Unlike(int id) {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (email == user.Email)
            {
                if (await _postRepo.UnlikePostAsync(id, user.Id))
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

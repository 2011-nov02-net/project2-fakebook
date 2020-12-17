using Fakebook.Domain.Repository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts/")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IPostRepo _postRepo;

        public LikeController(IPostRepo postRepo) {
            _postRepo = postRepo;
        }

        // POST: api/Posts/{id}/like/{userId}
        [HttpPost("{id}/like/{userId}")]
        public async Task<IActionResult> Like(int id, int userId)
        {
            // user is liking a post
            if(await _postRepo.LikePostAsync(id, userId)) {
                return Ok("Success");
            } else {
                return BadRequest();
            }
        }

        // POST: api/Posts/{id}/unlike/{userId}
        [HttpPost("{id}/unlike/{userId}")]
        public async Task<IActionResult> Unlike(int id, int userId) {
            // user is unliking a post
            if (await _postRepo.UnlikePostAsync(id, userId)) {
                return Ok("Success");
            } else {
                return BadRequest();
            }
        }
    }
}

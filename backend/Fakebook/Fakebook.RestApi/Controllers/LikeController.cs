﻿using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts/")]
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
        [HttpPost("{id}/like/{userId}")]
        [Authorize]
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
        [Authorize]
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

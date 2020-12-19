using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Newsfeed")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICommentRepo _commentRepo;

        public FeedController(IPostRepo postRepo, IUserRepo userRepo, ICommentRepo commentRepo)
        {
            _postRepo = postRepo;
            _userRepo = userRepo;
            _commentRepo = commentRepo;
        }
        // GET: api/<Feed>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            User.Claims.First(c => c.Type.Contains("Email"));
            var posts = await _postRepo.GetFollowingPosts(id);
            return Ok(posts);
        }
    }
}

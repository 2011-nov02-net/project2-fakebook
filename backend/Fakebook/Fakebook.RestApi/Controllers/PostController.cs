using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;

        public PostController(IPostRepo postRepo, IUserRepo userRepo)
        {
            _postRepo = postRepo;
            _userRepo = userRepo;
        }
        // Gets all posts. Use this for newsfeed
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postRepo.GetAllPosts();
            return Ok(posts);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            if (await _postRepo.CreatePost(post))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Post post)
        {
            if (post.IsValid())
            {
                await _postRepo.UpdatePost(post);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _postRepo.DeletePost(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

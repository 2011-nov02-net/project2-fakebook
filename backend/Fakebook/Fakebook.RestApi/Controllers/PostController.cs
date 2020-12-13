using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/[controller]")]
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
        // Gets all posts by a specific user id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var posts = await _postRepo.GetPostsByUserId(id);
            return Ok(posts);
        }
    }
}

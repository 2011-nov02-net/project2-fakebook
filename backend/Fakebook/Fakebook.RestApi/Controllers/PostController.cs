using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<IActionResult> GetUserPosts(int id)
        {
            var posts = await _postRepo.GetPostsByUserId(id);
            return Ok(posts);
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsfeedPosts(int id)
        {
            var currentUser = await _userRepo.GetUserById(id);
            var result = new List<Post>();
            foreach(var followee in currentUser.Followees) // Iterate through list of people user follows
            {
                if (followee.Posts != null) // Check if the followee has made any posts
                {
                    var followeePosts = followee.Posts;
                    if (followeePosts.Count < 3) // If followee has less than 3 posts, add all posts to result
                    {
                        foreach (var post in followeePosts)
                        {
                            result.Add(post);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < 3; i++) // Add up to 3 posts from the followee to the result
                        {
                            result.Add(followeePosts.ElementAt(i));
                        }
                    }
                }
            }
            result.OrderBy(p => p.CreatedAt); // Order list by the date the posts were created
            return Ok(result);
        }
    }
}

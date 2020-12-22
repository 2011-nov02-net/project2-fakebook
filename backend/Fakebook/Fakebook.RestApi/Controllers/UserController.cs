using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Fakebook.RestApi.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Http;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    // /api/user
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPostRepo _postRepo;

        public UserController(IUserRepo repository, IPostRepo postRepo) {
            _userRepo = repository;
            _postRepo = postRepo;
        }

        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns></returns>
        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get() {
            IEnumerable<User> users = await _userRepo.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<User>>> Search(string name)
        {
            IEnumerable<User> users = await _userRepo.GetUserByName(name);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id) {
            User user = await _userRepo.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound();
            }
            return user;
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            User user = await _userRepo.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        // user/profile
        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<User>> GetUserProfile()
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
        // Gets all posts by a specific user id
        [HttpGet("{id}/Posts")]
        public async Task<ActionResult<List<PostApiModel>>> GetUserPosts(int id) {
            List<Post> posts = await _postRepo.GetPostsByUserIdAsync(id);
            return posts
                .Select(p => ApiModelConverter.ToPostApiModel(p))
                .ToList();
        }

        [HttpGet("Posts")]
        [Authorize]
        public async Task<ActionResult<List<PostApiModel>>> GetUserPosts()
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            List<Post> posts = await _postRepo.GetPostsByUserIdAsync(user.Id);
            return posts
                .Select(p => ApiModelConverter.ToPostApiModel(p))
                .ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserApiModel apiModel) {
            try {
                var user = ApiModelConverter.ToUser(_userRepo, apiModel);

                int result = await _userRepo.CreateUser(user);

                if (result != -1) {
                    // return Created();
                    return Ok();
                } else {
                    return BadRequest();
                }
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// A delete method that wlil return False if it can't find that id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email.ToLower() == user.Email.ToLower()) {
                await _userRepo.DeleteUserAsync(id);
                return Ok();
            } else {
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(UserApiModel apiModel, int id = -1)
        {
            var user = await _userRepo.GetUserByIdAsync(apiModel.Id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            // if the id is null switch to bad request
            if (email.ToLower() == user.Email.ToLower()) {
                try {
                    if (id == -1) {
                        throw new ArgumentException("id cannot be -1");
                    }

                    var result = ApiModelConverter.ToUser(_userRepo, apiModel);
                    await _userRepo.UpdateUserAsync(id, result);
                    return Ok();
                } catch (ArgumentException ex) {
                    return BadRequest(ex.Message);
                } catch {
                    return BadRequest();
              }
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
            var user = await _userRepo.GetUserByEmail(email);
            return Ok(user);
        }
        */

        /*
        [HttpGet("{id}/Newsfeed")]
        [Authorize]
        public async Task<IActionResult> GetNewsfeedPosts(int id)
        {
            var currentUser = await _userRepo.GetUserByIdAsync(id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var result = new List<Post>();
            var userPosts = currentUser.Posts.OrderBy(p => p.CreatedAt).ToList();
            if (userPosts.Count < 3) // Check for any self user posts to include in newsfeed
            {
                foreach (var post in userPosts) {
                    result.Add(post);
                }
            } else {
                for (var i = 0; i < 3; i++) // Add up to 3 posts from self user to the result
                {
                    result.Add(userPosts.ElementAt(i));
                }
            }
            foreach (var followee in currentUser.Followees) // Iterate through list of people user follows
            {
                if (followee.Posts != null) // Check if the followee has made any posts
                {
                    var followeePosts = followee.Posts.OrderBy(p => p.CreatedAt).ToList();
                    if (followeePosts.Count < 3) // If followee has less than 3 posts, add all posts to result
                    {
                        foreach (var post in followeePosts) {
                            result.Add(post);
                        }
                    } else {
                        for (var i = 0; i < 3; i++) // Add up to 3 posts from the followee to the result
                        {
                            result.Add(followeePosts.ElementAt(i));
                        }
                    }
                }
            }
            result = result.OrderBy(p => p.CreatedAt).ToList(); // Order list by the date the posts were created
            return Ok(result);
        }
        */
    }
}

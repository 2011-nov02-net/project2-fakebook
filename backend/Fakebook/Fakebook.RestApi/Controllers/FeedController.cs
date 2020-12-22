using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Fakebook.RestApi.Model;

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

        public FeedController(IPostRepo postRepo, IUserRepo userRepo, ICommentRepo commentRepo) {
            _postRepo = postRepo;
            _userRepo = userRepo;
            _commentRepo = commentRepo;
        }
        // GET: api/<Feed>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id) {
            try {
                var user = await _userRepo.GetUserByIdAsync(id);
                var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                if (email.ToLower() == user.Email.ToLower()) {
                    var posts = await _postRepo.GetFollowingPosts(id);
                    return Ok(posts.Select(p => ApiModelConverter.ToPostApiModel(p)));
                } else {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            } catch {
                return BadRequest();
            }
        }
        // GET: api/<Feed>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var user = await _userRepo.GetUserByEmailAsync(email);
                if (user != null)
                {
                    var posts = await _postRepo.GetFollowingPosts(user.Id);
                    return Ok(posts.Select(p => ApiModelConverter.ToPostApiModel(p)));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

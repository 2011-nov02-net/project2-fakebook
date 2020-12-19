using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fakebook.RestApi.Model;
using System;
using Microsoft.AspNetCore.Authorization;
using Fakebook.Domain;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICommentRepo _commentRepo;

        public PostController(IPostRepo postRepo, IUserRepo userRepo, ICommentRepo commentRepo)
        {
            _postRepo = postRepo;
            _userRepo = userRepo;
            _commentRepo = commentRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postRepo.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(PostApiModel apiModel)
        {
            try {
                var post = ApiModelConverter.ToPost(_userRepo, _commentRepo, apiModel);

                if(await _postRepo.CreatePostAsync(post)) {
                    // return Created()
                    return Ok();
                } else {
                    return BadRequest();
                }
            } catch(ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(PostApiModel apiModel)
        {
            try {
                var post = ApiModelConverter.ToPost(_userRepo, _commentRepo, apiModel);
                await _postRepo.UpdatePostAsync(post);
                return Ok();
            } catch(ArgumentException ex) {
                return BadRequest(ex.Message);
            } catch {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _postRepo.DeletePostAsync(id))
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

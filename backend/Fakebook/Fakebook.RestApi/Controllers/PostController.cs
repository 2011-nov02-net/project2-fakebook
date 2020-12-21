using Fakebook.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fakebook.RestApi.Model;
using System;
using Microsoft.AspNetCore.Authorization;
using Fakebook.Domain;
using System.Linq;
using Fakebook.RestApi.Services;
using Microsoft.AspNetCore.Http;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICommentRepo _commentRepo;
        private IBlobService _blobService;

        public PostController(IPostRepo postRepo, IUserRepo userRepo, ICommentRepo commentRepo, IBlobService blobService) {
            _postRepo = postRepo;
            _userRepo = userRepo;
            _commentRepo = commentRepo;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var posts = await _postRepo.GetAllPostsAsync();
            return Ok(posts.Select(p => ApiModelConverter.ToPostApiModel(p)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(PostApiModel apiModel)
        {
            var user = await _userRepo.GetUserByIdAsync(apiModel.User.Id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email == user.Email) {
                try {
                    var post = ApiModelConverter.ToPost(_userRepo, _commentRepo, apiModel);

                    int result = await _postRepo.CreatePostAsync(post);

                    if (result != -1) {
                        // return Created()
                        return Ok();
                    } else {
                        return BadRequest();
                    }
                } catch (ArgumentException ex) {
                    return BadRequest(ex.Message);
                }
            } else {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(PostApiModel apiModel)
        {
            var user = await _userRepo.GetUserByIdAsync(apiModel.User.Id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email == user.Email) {
                try {
                    var post = ApiModelConverter.ToPost(_userRepo, _commentRepo, apiModel);
                    await _postRepo.UpdatePostAsync(post);
                    return Ok();
                } catch (ArgumentException ex) {
                    return BadRequest(ex.Message);
                } catch {
                    return BadRequest();
                }
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            var post = await _postRepo.GetPostByIdAsync(id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email == post.User.Email) {
                await _postRepo.DeletePostAsync(id);
                return Ok();
            } else {
                return BadRequest();
            }
        }

        [HttpPost("UploadPicture"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPicture()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await _blobService.UploadFileBlobAsync(
                    "Fakebook",
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

            var toReturn = result.AbsoluteUri;

            return Ok(new { path = toReturn });
        }
    }
}

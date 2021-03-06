﻿using Fakebook.Domain.Repository;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);
            return Ok(ApiModelConverter.ToPostApiModel(post));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(PostApiModel apiModel) {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (email.ToLower() == user.Email.ToLower()) {
                try {
                    apiModel.User = ApiModelConverter.ToUserApiModel(user);
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
        public async Task<IActionResult> Put(PostApiModel apiModel) {
            var user = await _userRepo.GetUserByIdAsync(apiModel.User.Id);
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email.ToLower() == user.Email.ToLower()) {
                try {
                    apiModel.User = ApiModelConverter.ToUserApiModel(user);
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
            await _postRepo.DeletePostAsync(id);
            return Ok();
        }

        [HttpPost("UploadPicture"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPicture() {
            IFormFile file = Request.Form.Files[0];
            if (file == null) {
                return BadRequest();
            }
            try {
                // generate a random guid from the file name
                string extension = file
                    .FileName
                        .Split('.')
                        .Last();
                string newFileName = $"{Request.Form["userId"]}-{Guid.NewGuid()}.{extension}";

                var result = await _blobService.UploadFileBlobAsync(
                        "fakebook",
                        file.OpenReadStream(),
                        file.ContentType,
                        newFileName);

                var toReturn = result.AbsoluteUri;

                return Ok(new { path = toReturn });
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

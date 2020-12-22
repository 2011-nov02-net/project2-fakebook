using System;
using System.Threading.Tasks;

using Fakebook.Domain;
using Fakebook.Domain.Repository;
using Fakebook.RestApi.Model;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.RestApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPostRepo _postRepo;

        public CommentController(ICommentRepo commentRepo, IUserRepo userRepo, IPostRepo postRepo) {
            _commentRepo = commentRepo;
            _userRepo = userRepo;
            _postRepo = postRepo;
        }

        [HttpPost("{id}/Comment")]
        [Authorize]
        public async Task<IActionResult> Post(CommentApiModel apiModel) {
            try {
                var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var user = await _userRepo.GetUserByEmailAsync(email);

                apiModel.User = ApiModelConverter.ToUserApiModel(user);

                Comment comment = ApiModelConverter.ToComment(_commentRepo, _userRepo, _postRepo, apiModel);

                int result = await _commentRepo.CreateAsync(comment);

                if (result != -1) {
                    return Ok();
                } else {
                    throw new ArgumentException("Could not create comment");
                }
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/Comment")]
        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (await _commentRepo.DeleteAsync(id)) {
                    return Ok();
                } else {
                    throw new ArgumentException("Could not create comment");
                }
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            } catch {
                return BadRequest();
            }
        }
    }
}

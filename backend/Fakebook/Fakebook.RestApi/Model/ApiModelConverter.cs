using System.Linq;

using Fakebook.Domain;
using Fakebook.Domain.Extension;
using Fakebook.Domain.Repository;

namespace Fakebook.RestApi.Model
{
    public class ApiModelConverter
    {
        public static Post ToPost(IUserRepo userRepo, ICommentRepo commentRepo, PostApiModel apiModel) {
            apiModel.Content.NullOrEmptyCheck(nameof(apiModel.Content));

            var user = userRepo.GetUserById(apiModel.UserId).Result;

            var likedByUsers = userRepo.GetUsersByIds(apiModel.LikedByUserIds)
                .Result
                .ToList();

            var comments = commentRepo.GetCommentsByIdsAsync(apiModel.CommentIds)
                .Result
                .ToList();

            return new Post
            {
                Id = apiModel.Id,
                Content = apiModel.Content,
                CreatedAt = apiModel.CreatedAt,
                Picture = apiModel.Picture,
                User = user,
                LikedByUsers = likedByUsers,
                Comments = comments
            };
        }

        public static User ToUser(IUserRepo userRepo, UserApiModel apiModel) {
            apiModel.FirstName.NullOrEmptyCheck(nameof(apiModel.FirstName));
            apiModel.LastName.NullOrEmptyCheck(nameof(apiModel.LastName));
            apiModel.Email.NullOrEmptyCheck(nameof(apiModel.Email));

            var followers = userRepo.GetUsersByIds(apiModel.FollowerIds)
                .Result
                .ToList();

            var followees = userRepo.GetUsersByIds(apiModel.FolloweeIds)
                .Result
                .ToList();

            return new User
            {
                Id = apiModel.Id,
                ProfilePictureUrl = apiModel.ProfilePictureUrl,
                FirstName = apiModel.FirstName,
                LastName = apiModel.LastName,
                Email = apiModel.Email,
                PhoneNumber = apiModel.PhoneNumber,
                BirthDate = apiModel.BirthDate,
                Status = apiModel.Status,
                Followers = followers,
                Followees = followees
            };
        }

        public static Comment ToComment(ICommentRepo commentRepo, IUserRepo userRepo, IPostRepo postRepo, CommentApiModel apiModel) {
            apiModel.Content.NullOrEmptyCheck(nameof(apiModel.Content));

            var user = userRepo.GetUserById(apiModel.UserId)
                .Result;

            var post = postRepo.GetPostById(apiModel.PostId)
                .Result;

            var childComments = commentRepo.GetCommentsByIdsAsync(apiModel.ChildCommentIds)
                .Result
                .ToList();

            return new Comment
            {
                Id = apiModel.Id,
                Content = apiModel.Content,
                CreatedAt = apiModel.CreatedAt,
                User = user,
                Post = post,
                ChildrenComments = childComments
            };
        }
    }
}

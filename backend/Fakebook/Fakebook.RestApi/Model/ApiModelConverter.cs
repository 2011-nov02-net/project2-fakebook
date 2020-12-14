using System.Collections.Generic;
using System.Linq;

using Fakebook.Domain;
using Fakebook.Domain.Extension;
using Fakebook.Domain.Repository;

namespace Fakebook.RestApi.Model
{
    public static class ApiModelConverter
    {
        public static Post ToPost(IUserRepo userRepo, ICommentRepo commentRepo, PostApiModel apiModel) {
            apiModel.Content.NullOrEmptyCheck(nameof(apiModel.Content));

            var user = userRepo.GetUserByIdAsync(apiModel.UserId).Result;
            List<Comment> comments = null;
            List<User> likedByUsers = null;

            if(apiModel.CommentIds is not null && apiModel.CommentIds.Any()) {
                comments = commentRepo.GetCommentsByIdsAsync(apiModel.CommentIds)
                    .Result
                    .ToList();
            }

            if(apiModel.LikedByUserIds is not null && apiModel.LikedByUserIds.Any()) {
                likedByUsers = userRepo.GetUsersByIdsAsync(apiModel.LikedByUserIds)
                    .Result
                    .ToList();
            }

            comments ??= new List<Comment>();
            likedByUsers ??= new List<User>();

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

            List<User> followers = null;
            List<User> followees = null;

            if(apiModel.FolloweeIds is not null && apiModel.FolloweeIds.Any()) {
                followees = userRepo.GetUsersByIdsAsync(apiModel.FolloweeIds)
                    .Result
                    .ToList();
            }

            if(apiModel.FollowerIds is not null && apiModel.FollowerIds.Any()) {
                followers = userRepo.GetUsersByIdsAsync(apiModel.FollowerIds)
                    .Result
                    .ToList();
            }

            followees ??= new List<User>();
            followers ??= new List<User>();

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
            
            var user = userRepo.GetUserByIdAsync(apiModel.UserId)
                .Result;

            user.NullCheck(nameof(user));

            var post = postRepo.GetPostByIdAsync(apiModel.PostId)
                .Result;

            post.NullCheck(nameof(user));

            List<Comment> childComments = null;

            if(apiModel.ChildCommentIds is not null && apiModel.ChildCommentIds.Any()) {
                childComments = commentRepo.GetCommentsByIdsAsync(apiModel.ChildCommentIds)
                    .Result
                    .ToList();
            }

            childComments ??= new List<Comment>();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Fakebook.Domain;
using Fakebook.Domain.Extension;
using Fakebook.Domain.Repository;

namespace Fakebook.RestApi.Model
{
    public static class ApiModelConverter
    {
        public static Post ToPost(IUserRepo userRepo, ICommentRepo commentRepo, PostApiModel apiModel) {
            apiModel.Content.NullOrEmptyCheck(nameof(apiModel.Content));

            // if content is not null, filter out any special characters
            var regex = new Regex(RegularExpressions.NoSpecialCharacters);
            if (apiModel.Content is not null && !regex.IsMatch(apiModel.Content)) {
                throw new ArgumentException("No special characters are permitted in the content.");
            }

            // must not be in future
            if (apiModel.CreatedAt > DateTime.Now) {
                throw new ArgumentException("Date is in the future.");
            }

            // if status is not null, filter out any non-file allowed characters
            regex = new Regex(RegularExpressions.NoSpecialCharacters);
            if (apiModel.Picture is not null && !regex.IsMatch(apiModel.Picture)) {
                throw new ArgumentException("No special characters are permitted in the picture URL.");
            }

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

            var regex = new Regex(RegularExpressions.NameCharacters);
            if (!regex.IsMatch(apiModel.FirstName)) {
                throw new ArgumentException("First name can only contain name characters.");
            }

            regex = new Regex(RegularExpressions.NameCharacters);
            if (!regex.IsMatch(apiModel.LastName)) {
                throw new ArgumentException("Last name can only contain name characters.");
            }

            // must match email regex
            regex = new Regex(RegularExpressions.EmailCharacters);
            if (!regex.IsMatch(apiModel.Email)) {
                throw new ArgumentException("Email isn't a valid email.");
            }

            // must match phone number regex
            regex = new Regex(RegularExpressions.PhoneNumberCharacters);
            if (apiModel.PhoneNumber is not null && !regex.IsMatch(apiModel.PhoneNumber)) {
                throw new ArgumentException("Phone Number isn't a valid phone number.");
            }

            // must not be in future
            // must also be within 18 years from today
            var today = DateTime.Today;
            var date = new DateTime(today.Year - 18, today.Month, today.Day);
            if (apiModel.BirthDate > date) {
                throw new ArgumentException("Date is greater than 18 years ago from today.");
            }

            // if status is not null, filter out any special characters
            regex = new Regex(RegularExpressions.NoSpecialCharacters);
            if (apiModel.Status is not null && regex.IsMatch(apiModel.Status)) {
                throw new ArgumentException("Status cannot contain any special characters");
            }

            // if status is not null, filter out any non-file allowed characters
            regex = new Regex(RegularExpressions.NoSpecialCharacters);
            if (apiModel.ProfilePictureUrl is not null && regex.IsMatch(apiModel.ProfilePictureUrl)) {
                throw new ArgumentException("Picture URL cannot contain any special characters");
            }

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

            // if content is not null, filter out any special characters
            var regex = new Regex(RegularExpressions.NoSpecialCharacters);
            if (apiModel.Content is not null && regex.IsMatch(apiModel.Content)) {
                throw new ArgumentException("No special characters are permitted in the content.");
            }

            // must not be in future
            if (apiModel.CreatedAt > DateTime.Now) {
                throw new ArgumentException("Date is in the future.");
            }

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

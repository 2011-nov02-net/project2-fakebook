﻿using System;
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
        public static PostApiModel ToPostApiModel(Post post) {

            var commentIds = post.Comments
                .Select(c => c.Id)
                .ToList();

            var likedByUserIds = post.LikedByUsers
                .Select(u => u.Id)
                .ToList();

            var comments = post.Comments
                .Select(c => {
                    c.Post = post;
                    return ToCommentApiModel(c);
                })
                .ToList();

            return new PostApiModel
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                PictureUrl = post.Picture,
                User = ToUserApiModel(post.User),
                Comments = comments,
                LikedByUserIds = likedByUserIds
            };
        }

        public static Post ToPost(IUserRepo userRepo, ICommentRepo commentRepo, PostApiModel apiModel) {
            apiModel.Content.EnforceNoSpecialCharacters(nameof(apiModel.Content));

            // if status is not null, filter out any non-file allowed characters
            if (!apiModel.PictureUrl.IsNullOrEmpty()) {
                apiModel.PictureUrl.EnforceNoSpecialCharacters(nameof(apiModel.PictureUrl));
            }
            var user = userRepo.GetUserByIdAsync(apiModel.User.Id).Result;
            List<Comment> comments = null;
            List<User> likedByUsers = null;

            if(apiModel.Comments is not null && apiModel.Comments.Any()) {
                var commentIds = apiModel
                    .Comments
                        .Select(c => c.Id)
                        .ToList();

                comments = commentRepo.GetCommentsByIdsAsync(commentIds)
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
                Picture = apiModel.PictureUrl,
                User = user,
                LikedByUsers = likedByUsers,
                Comments = comments
            };
        }

        public static UserApiModel ToUserApiModel(User user) {

            var followerIds = user.Followers
                .Select(f => f.Id)
                .ToList();

            var followeeIds = user.Followees
                .Select(f => f.Id)
                .ToList();

            return new UserApiModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Status = user.Status,
                ProfilePictureUrl = user.ProfilePictureUrl,
                PhoneNumber = user.PhoneNumber,
                FolloweeIds = followeeIds,
                FollowerIds = followerIds
            };
        }

        public static User ToUser(IUserRepo userRepo, UserApiModel apiModel) {
            apiModel.FirstName.EnforceNameCharacters(nameof(apiModel.FirstName));
            apiModel.LastName.EnforceNameCharacters(nameof(apiModel.LastName));
            apiModel.Email.EnforceEmailCharacters(nameof(apiModel.Email));

            // must match phone number regex
            if(!apiModel.PhoneNumber.IsNullOrEmpty()) {
                apiModel.PhoneNumber.EnforcePhoneNumberCharacters(nameof(apiModel.PhoneNumber));
            }
            
            if (apiModel.Status is not null) {
                apiModel.Status.EnforceNoSpecialCharacters(nameof(apiModel.Status));
            }

            // if status is not null, filter out any non-file allowed characters
            /*
            if (apiModel.ProfilePictureUrl is not null) {
                apiModel.ProfilePictureUrl.EnforceNoSpecialCharacters(nameof(apiModel.ProfilePictureUrl));
            }
            */
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

        public static CommentApiModel ToCommentApiModel(Comment comment) {

            var childCommentIds = comment.ChildrenComments
                    .Select(c => c.Id)
                    .ToList();

            return new CommentApiModel
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                PostId = comment.Post.Id,
                User = ToUserApiModel(comment.User),
                ParentCommentId = comment.ParentComment?.Id,
                ChildCommentIds = childCommentIds
            };
        }

        public static Comment ToComment(ICommentRepo commentRepo, IUserRepo userRepo, IPostRepo postRepo, CommentApiModel apiModel) {
            // no special characters are allowed
            apiModel.Content.EnforceNoSpecialCharacters(nameof(apiModel.Content));

            var user = userRepo.GetUserByIdAsync(apiModel.User.Id)
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

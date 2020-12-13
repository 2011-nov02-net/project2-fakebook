using System;
using System.Collections.Generic;
using System.Linq;

using Fakebook.DataAccess.Model;

namespace Fakebook.Domain
{
    public static class DbEntityConverter
    {
        public static UserEntity ToUserEntity(User user) {
            // this would presume that .Include/.ThenInclude was called

            List<FollowEntity> followees = null;
            List<FollowEntity> followers = null;

            if (user.Followees.Any()) {
                followees = user.Followees
                    .Select(u => {
                        return new FollowEntity
                        {
                            FollowerId = user.Id,
                            FolloweeId = u.Id
                        };
                    })
                    .ToList();
            }

            if (user.Followers.Any()) {
                followers = user.Followers
                    .Select(u => {
                        return new FollowEntity
                        {
                            FollowerId = u.Id,
                            FolloweeId = user.Id
                        };
                    })
                    .ToList();
            }

            followees ??= new List<FollowEntity>();
            followers ??= new List<FollowEntity>();

            return new UserEntity
            {
                Id = user.Id,
                ProfilePictureUrl = user.ProfilePictureUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Status = user.Status,
                Followees = followees,
                Followers = followers
            };
        }

        public static User ToUser(UserEntity userEntity) {

            List<User> followees = null;
            List<User> followers = null;

            if (userEntity.Followees.Any()) {
                followees = userEntity.Followees
                    .Select(f => f.Followee)
                    .Where(u => u is not null)
                    .Select(u => ToUser(u))
                    .ToList();
            }

            if (userEntity.Followers.Any()) {
                followees = userEntity.Followers
                    .Select(f => f.Follower)
                    .Where(u => u is not null)
                    .Select(u => ToUser(u))
                    .ToList();
            }

            followees ??= new List<User>();
            followers ??= new List<User>();

            return new User
            {
                Id = userEntity.Id,
                ProfilePictureUrl = userEntity.ProfilePictureUrl,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber,
                BirthDate = userEntity.BirthDate,
                Status = userEntity.Status,
                Followees = followees,
                Followers = followers
            };
        }
    
        public static PostEntity ToPostEntity(Post post) {
            return default;
        }

        public static Post ToPost(PostEntity postEntity) {
            return default;
        }

        public static CommentEntity ToCommentEntity(Comment comment) {
            return new CommentEntity
            {
                Id = comment.Id,
                UserId = comment.User.Id,
                PostId = comment.Post.Id,
                ParentId = comment.ParentComment?.Id,
                CreatedAt = comment.CreatedAt,
                Content = comment.Content,
                User = ToUserEntity(comment.User)
            };
        }

        public static Comment ToComment(CommentEntity commentEntity) {
            return new Comment
            {
                Id = commentEntity.Id,
                Content = commentEntity.Content,
                CreatedAt = commentEntity.CreatedAt,
                Post = ToPost(commentEntity.Post),
                User = ToUser(commentEntity.User),
#pragma warning This could cause an issue with recursion, although it only goes until this is null
                ParentComment = ToComment(commentEntity.ParentComment)
            };
        }

        public static List<FollowEntity> ToFollowEntities(User user) {
            return user.Followers.Select(u => {
                return new FollowEntity
                {
                    FollowerId = u.Id,
                    FolloweeId = user.Id
                };
            })
            .ToList();
        }

        public static List<LikeEntity> ToLikeEntities(Post post) {
            return post.LikedByUsers.Select(u => {
                return new LikeEntity
                {
                    UserId = u.Id,
                    PostId = post.Id
                };
            })
            .ToList();
        }
    }
}

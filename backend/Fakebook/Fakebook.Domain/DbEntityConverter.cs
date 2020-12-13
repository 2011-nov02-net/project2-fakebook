using System;
using System.Collections.Generic;
using System.Linq;

using Fakebook.DataAccess.Model;
using Fakebook.Domain.Extension;

namespace Fakebook.Domain
{
    public static class DbEntityConverter
    {
        public static UserEntity ToUserEntity(User user) {
            user.NullCheck(nameof(user));
            user.Followees.NullCheck(nameof(user.Followees));
            user.Followers.NullCheck(nameof(user.Followers));

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
            userEntity.NullCheck(nameof(userEntity));
            userEntity.Followees.NullCheck(nameof(userEntity.Followees));
            userEntity.Followers.NullCheck(nameof(userEntity.Followers));

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
            List<CommentEntity> comments = null;
            List<LikeEntity> likes = null;

            if (post.Comments.Any())
            {
                comments = post.Comments
                    .Select(p => {
                        return new CommentEntity
                        {
                            Id = p.Id,
                            UserId = p.User.Id,
                            PostId = p.Post.Id,
                            ParentId = p.ParentComment.Id,
                            CreatedAt = p.CreatedAt,
                            Content = p.Content
                        };
                    })
                    .ToList();
            }
            if (post.LikedByUsers.Any())
            {
                likes = ToLikeEntities(post);
            }
            return new PostEntity
            {
                Id = post.Id,
                UserId = post.User.Id,
                Content = post.Content,
                Picture = post.Picture,
                CreatedAt = post.CreatedAt,
                Comments = comments,
                Likes = likes,
            };
        }

        public static Post ToPost(PostEntity postEntity) {
            List<Comment> comments = null;
            List<User> users = null;

            if (postEntity.Comments.Any())
            {
                comments = postEntity.Comments
                    .Select(c => ToComment(c))
                    .ToList();
            }
            if (postEntity.Likes.Any())
            {
                users = postEntity.Likes
                    .Select(l => ToUser(l.User))
                    .ToList();
            }
            return new Post
            {
                Id = postEntity.Id,
                Content = postEntity.Content,
                Picture = postEntity.Picture,
                CreatedAt = postEntity.CreatedAt,
                User = ToUser(postEntity.User),
                LikedByUsers = users,
                Comments = comments
            };
        }

        public static CommentEntity ToCommentEntity(Comment comment) {
            comment.NullCheck(nameof(comment));
            comment.User.NullCheck(nameof(comment.User));

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
            commentEntity.NullCheck(nameof(commentEntity));
            commentEntity.Post.NullCheck(nameof(commentEntity.Post));
            commentEntity.User.NullCheck(nameof(commentEntity.User));

            var parentComment = commentEntity.ParentComment is not null
                ? ToComment(commentEntity.ParentComment)
                : null;

            return new Comment
            {
                Id = commentEntity.Id,
                Content = commentEntity.Content,
                CreatedAt = commentEntity.CreatedAt,
                Post = ToPost(commentEntity.Post),
                User = ToUser(commentEntity.User),
                ParentComment = parentComment
            };
        }

        public static List<FollowEntity> ToFollowEntities(User user) {
            user.NullCheck(nameof(user));
            user.Followers.NullCheck(nameof(user.Followers));

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
            post.NullCheck(nameof(post));
            post.LikedByUsers.NullCheck(nameof(post.LikedByUsers));

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

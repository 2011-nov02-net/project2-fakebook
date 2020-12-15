using System;
using System.Collections.Generic;
using System.Linq;

using Fakebook.DataAccess.Model;
using Fakebook.Domain.Extension;
using Fakebook.Domain.Repository;

namespace Fakebook.Domain
{
    public static class DbEntityConverter
    {
        public static UserEntity ToUserEntity(User user, int rabbitHoles = 0) {
            List<FollowEntity> followees = null;
            List<FollowEntity> followers = null;

            if (rabbitHoles > 0) {
                user.NullCheck(nameof(user));
                user.Followees.NullCheck(nameof(user.Followees));
                user.Followers.NullCheck(nameof(user.Followers));

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
            } else if (user is null) {
                return null;
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

        public static User ToUser(UserEntity userEntity, int rabbitHoles = 0) {
            List<User> followees = null;
            List<User> followers = null;

            if (rabbitHoles > 0) {
                userEntity.NullCheck(nameof(userEntity));
                userEntity.Followees.NullCheck(nameof(userEntity.Followees));
                userEntity.Followers.NullCheck(nameof(userEntity.Followers));

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
            } else if (userEntity is null) {
                return null;
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

        public static PostEntity ToPostEntity(Post post, int rabbitHoles = 0) {
            List<CommentEntity> comments = null;
            List<LikeEntity> likes = null;

            if (rabbitHoles > 0) {
                if (post.Comments is not null && post.Comments.Any()) {
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

                if (post.LikedByUsers is not null && post.LikedByUsers.Any()) {
                    likes = ToLikeEntities(post);
                }
            } else if (post is null) {
                return null;
            }

            comments ??= new List<CommentEntity>();
            likes ??= new List<LikeEntity>();

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
            var result = new Post
            {
                Id = postEntity.Id,
                Content = postEntity.Content,
                Picture = postEntity.Picture,
                CreatedAt = postEntity.CreatedAt,
                User = ToUser(postEntity.User),
                LikedByUsers = new List<User>(),
                Comments = new List<Comment>()
            };
            if (postEntity.Comments.Any())
            {
                var comments = postEntity.Comments;
                foreach (var comment in comments) // See if there are any comments for the post
                {
                    var newComment = new Comment()
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        User = new User()
                        {
                            Id = comment.User.Id,
                            ProfilePictureUrl = comment.User.ProfilePictureUrl,
                            FirstName = comment.User.FirstName,
                            LastName = comment.User.LastName
                        },
                        CreatedAt = comment.CreatedAt,
                        ChildrenComments = new List<Comment>()
                    };
                    if (comment.ChildrenComments != null)
                    {
                        foreach(var child in comment.ChildrenComments)
                        {
                            var newChild = new Comment()
                            {
                                Id = child.Id,
                                Content = child.Content,
                                User = new User()
                                {
                                    Id = child.User.Id,
                                    ProfilePictureUrl = child.User.ProfilePictureUrl,
                                    FirstName = child.User.FirstName,
                                    LastName = child.User.LastName
                                }
                            };
                            newComment.ChildrenComments.Add(newChild);
                        }
                    }
                    result.Comments.Add(newComment);
                }
            }
            if (postEntity.Likes.Any())
            {
                var likes = postEntity.Likes;
                foreach (var like in likes)
                {
                    var newUser = new User()
                    {
                        Id = like.User.Id,
                        ProfilePictureUrl = like.User.ProfilePictureUrl,
                        FirstName = like.User.FirstName,
                        LastName = like.User.LastName
                    };
                    result.LikedByUsers.Add(newUser);
                };
            }
            return result;
        }

        public static CommentEntity ToCommentEntity(Comment comment, int rabbitHoles = 0) {
            if (rabbitHoles > 0) {
                comment.NullCheck(nameof(comment));
                comment.User.NullCheck(nameof(comment.User));
            } else if(comment is null) {
                return null;
            }

            return new CommentEntity
            {
                Id = comment.Id,
                UserId = comment.User.Id,
                PostId = comment.Post.Id,
                ParentId = comment.ParentComment?.Id,
                CreatedAt = comment.CreatedAt,
                Content = comment.Content,
                User = ToUserEntity(comment.User, rabbitHoles - 1)
            };
        }

        public static Comment ToComment(CommentEntity commentEntity, int rabbitHoles = 0) {
            Comment parentComment = null;

            if (rabbitHoles > 0) {
                commentEntity.NullCheck(nameof(commentEntity));
                parentComment = ToComment(commentEntity.ParentComment, rabbitHoles - 1);
            } else if (commentEntity is null) {
                return null;
            }

            return new Comment
            {
                Id = commentEntity.Id,
                Content = commentEntity.Content,
                CreatedAt = commentEntity.CreatedAt,
                Post = ToPost(commentEntity.Post),
                User = ToUser(commentEntity.User, rabbitHoles - 1),
                ParentComment = parentComment
            };
        }

        public static List<FollowEntity> ToFollowEntities(User user, int rabbitHoles = 0) {
            if (rabbitHoles > 0) {
                user.NullCheck(nameof(user));
                user.Followers.NullCheck(nameof(user.Followers));
            } else if (user is null) {
                return null;
            }

            return user.Followers.Select(u => {
                return new FollowEntity
                {
                    FollowerId = u.Id,
                    FolloweeId = user.Id
                };
            })
            .ToList();
        }

        public static List<LikeEntity> ToLikeEntities(Post post, int rabbitHoles = 0) {
            if (rabbitHoles > 0) {
                post.NullCheck(nameof(post));
                post.LikedByUsers.NullCheck(nameof(post.LikedByUsers));
            } else if (post is null) {
                return null;
            }

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

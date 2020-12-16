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
        public static UserEntity ToUserEntity(User user) {
            var result = new UserEntity
            {
                Id = user.Id,
                ProfilePictureUrl = user.ProfilePictureUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Status = user.Status,
                Followees = new List<FollowEntity>(),
                Followers = new List<FollowEntity>()
            };
            // Check for all people the user is following, assign a new follow entity with the followee as the person this user is following
            if (user.Followees != null) {
                var followees = user.Followees;
                foreach(var person in followees)
                {
                    var newFollowee = new FollowEntity()
                    {
                        FollowerId = result.Id,
                        FolloweeId = person.Id
                    };
                    result.Followees.Add(newFollowee);
                }
            };
            // Vice versa
            if (user.Followers != null)
            {
                var followers = user.Followers;
                foreach (var person in followers)
                {
                    var newFollower = new FollowEntity()
                    {
                        FollowerId = person.Id,
                        FolloweeId = result.Id
                    };
                    result.Followers.Add(newFollower);
                }
            };
            return result;
        }

        public static User ToUser(UserEntity userEntity) {
            var result = new User
            {
                Id = userEntity.Id,
                ProfilePictureUrl = userEntity.ProfilePictureUrl,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber,
                BirthDate = userEntity.BirthDate,
                Status = userEntity.Status,
                Followees = new List<User>(),
                Followers = new List<User>(),
                Posts = new List<Post>()
            };

            if(userEntity.Followees != null)
            {
                var followees = userEntity.Followees;
                foreach(var person in followees)
                {
                    var newFollowee = new User()
                    {
                        Id = person.FolloweeId,
                        ProfilePictureUrl = person.Followee.ProfilePictureUrl,
                        FirstName = person.Followee.FirstName,
                        LastName = person.Followee.LastName
                    };
                    result.Followees.Add(newFollowee);
                }
            }

            if (userEntity.Followers != null)
            {
                var followers = userEntity.Followers;
                foreach (var person in followers)
                {
                    var newFollower = new User()
                    {
                        Id = person.FollowerId,
                        ProfilePictureUrl = person.Follower.ProfilePictureUrl,
                        FirstName = person.Follower.FirstName,
                        LastName = person.Follower.LastName
                    };
                    result.Followers.Add(newFollower);
                }
            }

            if (userEntity.Posts != null)
            {
                var posts = userEntity.Posts;
                foreach (var post in posts)
                {
                    var newPost = new Post()
                    {
                        Id = post.Id,
                        Content = post.Content,
                        Picture = post.Picture,
                        CreatedAt = post.CreatedAt
                    };
                    result.Posts.Add(newPost);
                }
            }
            return result;
        }

        public static PostEntity ToPostEntity(Post post) {

            var result =  new PostEntity
            {
                Id = post.Id,
                UserId = post.User.Id,
                Content = post.Content,
                Picture = post.Picture,
                CreatedAt = post.CreatedAt,
                Comments = new List<CommentEntity>(),
            };

            if(post.Comments != null)
            {
                var comments = post.Comments;
                foreach(var comment in comments)
                {
                    var newComment = new CommentEntity
                    {
                        Id = comment.Id,
                        UserId = comment.User.Id,
                        PostId = comment.Post.Id,
                        ParentId = comment.ParentComment.Id,
                        CreatedAt = comment.CreatedAt,
                        Content = comment.Content
                    };
                    result.Comments.Add(newComment);
                }
            };

            if (post.LikedByUsers != null)
            {
                result.Likes = ToLikeEntities(post);
            };

            return result;
        }

        public static Post ToPost(PostEntity postEntity) {
            var result = new Post
            {
                Id = postEntity.Id,
                Content = postEntity.Content,
                //Picture = postEntity.Picture,
                CreatedAt = postEntity.CreatedAt,
                User = new User()
                {
                    Id = postEntity.User.Id,
                    FirstName = postEntity.User.FirstName,
                    LastName = postEntity.User.LastName,
                    ProfilePictureUrl = postEntity.User.ProfilePictureUrl
                },
                LikedByUsers = new List<User>(),
                Comments = new List<Comment>()
            };
            if (postEntity.Comments != null)
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
            if (postEntity.Likes != null)
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

        public static CommentEntity ToCommentEntity(Comment comment) {
            if(comment != null)
            {
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
            else
            {
                return null;
            }
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
                User = ToUser(commentEntity.User),
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

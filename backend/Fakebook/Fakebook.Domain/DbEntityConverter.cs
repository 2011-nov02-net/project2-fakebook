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
            user.NullCheck(nameof(user));
            user.FirstName.EnforceNameCharacters(nameof(user.FirstName));
            user.LastName.EnforceNameCharacters(nameof(user.LastName));
            user.Email.EnforceEmailCharacters(nameof(user.Email));

            if(!user.PhoneNumber.IsNullOrEmpty()) {
                user.PhoneNumber.EnforcePhoneNumberCharacters(nameof(user.PhoneNumber));
            }

            if(!user.Status.IsNullOrEmpty()) {
                user.Status.EnforceNoSpecialCharacters(nameof(user.Status));
            }

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
                foreach (var person in followees) {
                    var newFollowee = new FollowEntity()
                    {
                        FollowerId = result.Id,
                        FolloweeId = person.Id
                    };
                    result.Followees.Add(newFollowee);
                }
            };
            // Vice versa
            if (user.Followers != null) {
                var followers = user.Followers;
                foreach (var person in followers) {
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
            userEntity.NullCheck(nameof(UserEntity));
            userEntity.FirstName.EnforceNameCharacters(nameof(userEntity.FirstName));
            userEntity.LastName.EnforceNameCharacters(nameof(userEntity.LastName));
            userEntity.Email.EnforceEmailCharacters(nameof(userEntity.Email));

            if (!userEntity.PhoneNumber.IsNullOrEmpty()) {
                userEntity.PhoneNumber.EnforcePhoneNumberCharacters(nameof(userEntity.PhoneNumber));
            }

            if (!userEntity.Status.IsNullOrEmpty()) {
                userEntity.Status.EnforceNoSpecialCharacters(nameof(userEntity.Status));
            }

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

            if (userEntity.Followees != null) {
                var followees = userEntity.Followees;
                foreach (var person in followees) {
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

            if (userEntity.Followers != null) {
                var followers = userEntity.Followers;
                foreach (var person in followers) {
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

            if (userEntity.Posts != null) {
                var posts = userEntity.Posts;
                foreach (var post in posts) {
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
            post.NullCheck(nameof(post));
            post.Content.EnforceNoSpecialCharacters(nameof(post.Content));

            var result = new PostEntity
            {
                Id = post.Id,
                UserId = post.User.Id,
                Content = post.Content,
                Picture = post.Picture,
                CreatedAt = post.CreatedAt,
                Comments = new List<CommentEntity>(),
            };

            if (post.Comments != null) {
                var comments = post.Comments;
                foreach (var comment in comments) {
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

            if (post.LikedByUsers != null) {
                result.Likes = ToLikeEntities(post);
            };

            return result;
        }

        public static Post ToPost(PostEntity postEntity) {
            postEntity.NullCheck(nameof(postEntity));
            postEntity.Content.EnforceNoSpecialCharacters(nameof(postEntity.Content));

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
            if (postEntity.Comments != null) {
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
                            LastName = comment.User.LastName,
                            ProfilePictureUrl = comment.User.ProfilePictureUrl
                        },
                        CreatedAt = comment.CreatedAt,
                        ChildrenComments = new List<Comment>()
                    };
                    if (comment.ChildrenComments != null) {
                        foreach (var child in comment.ChildrenComments) {
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
            if (postEntity.Likes != null) {
                var likes = postEntity.Likes;
                foreach (var like in likes) {
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
            comment.NullCheck(nameof(comment));
            comment.Content.EnforceNoSpecialCharacters(nameof(comment.Content));

            if (comment != null) {
                return new CommentEntity
                {
                    Id = comment.Id,
                    UserId = comment.User.Id,
                    PostId = comment.Post.Id,
                    ParentId = comment.ParentComment?.Id,
                    CreatedAt = comment.CreatedAt,
                    Content = comment.Content,
                };
            } else {
                return null;
            }
        }

        public static Comment ToComment(CommentEntity commentEntity) {
            commentEntity.NullCheck(nameof(commentEntity));
            commentEntity.Content.EnforceNoSpecialCharacters(nameof(commentEntity.Content));

            var result = new Comment
            {
                Id = commentEntity.Id,
                Content = commentEntity.Content,
                CreatedAt = commentEntity.CreatedAt,
                Post = ToPost(commentEntity.Post),
                User = ToUser(commentEntity.User)
            };

            if (commentEntity.ParentComment != null) {
                var newComment = ToComment(commentEntity.ParentComment);
                result.ParentComment = newComment;
            };

            return result;
        }

        public static List<FollowEntity> ToFollowEntities(User user) {
            user.NullCheck(nameof(user));

            var result = new List<FollowEntity>();

            if (user.Followers != null) {
                foreach (var follower in user.Followers) {
                    var newFollower = new FollowEntity
                    {
                        FollowerId = follower.Id,
                        FolloweeId = user.Id
                    };
                    result.Add(newFollower);
                }
            };

            return result;
        }

        public static List<LikeEntity> ToLikeEntities(Post post) {
            post.NullCheck(nameof(post));

            var result = new List<LikeEntity>();

            if (post.LikedByUsers != null) {
                foreach (var like in post.LikedByUsers) {
                    var newLike = new LikeEntity
                    {
                        UserId = like.Id,
                        PostId = post.Id
                    };
                    result.Add(newLike);
                }
            };

            return result;
        }
    }
}

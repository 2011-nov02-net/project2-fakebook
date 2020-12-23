using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Domain;

namespace Fakebook.UnitTests.Repository.CommentTests
{
    public static class CommentTestData
    {
        public static class Create
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        Id = 1,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    var post = new Post
                    {
                        Id = 1,
                        User = user,
                        CreatedAt = GenerateRandom.DateTime(),
                        Content = GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 1,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 2,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 3,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 4,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    var post = new Post
                    {
                        Id = 1,
                        User = user,
                        CreatedAt = GenerateRandom.DateTime(),
                        Content = GenerateRandom.String()
                    };

                    // invalid content
                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 1,
                            Content = null,
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 1,
                            Content = "",
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 2,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = null,
                            Post = post
                        }
                    };

                    // null post
                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 3,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        }
                    };

                    // both null
                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 4,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = null,
                            Post = null
                        }
                    };

                    // both null + bad content
                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 5,
                            Content = null,
                            CreatedAt = GenerateRandom.DateTime(),
                            User = null,
                            Post = null
                        }
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 6,
                            Content = "",
                            CreatedAt = GenerateRandom.DateTime(),
                            User = null,
                            Post = null
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class ReadById
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        1
                    };

                    user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        GenerateRandom.Int(1, 2)
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        2
                    };

                    user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        -1
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class ReadByIds
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        new List<int> { 1 }
                    };

                    user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        new List<int>
                        {
                            GenerateRandom.Int(1, 3),
                            GenerateRandom.Int(1, 3)
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        Id = 1,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = 1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        2
                    };

                    user = new User
                    {
                        Id = 2,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = -1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Id = 2,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 2,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        -1
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Update
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        Id = 1,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    var post = new Post
                    {
                        Id = 1,
                        User = user,
                        CreatedAt = GenerateRandom.DateTime(),
                        Content = GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 1,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 2,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 3,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 4,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        GenerateRandom.String()
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    var post = new Post
                    {
                        Id = 1,
                        User = user,
                        CreatedAt = GenerateRandom.DateTime(),
                        Content = GenerateRandom.String()
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 1,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        null
                    };

                    yield return new object[]
                    {
                        new Comment
                        {
                            Id = 2,
                            Content = GenerateRandom.String(),
                            CreatedAt = GenerateRandom.DateTime(),
                            User = user,
                            Post = post
                        },
                        ""
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Delete
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        Id = 1,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = 1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        1
                    };

                    user = new User
                    {
                        Id = 2,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = 1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Id = 2,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 2,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        GenerateRandom.Int(1, 2)
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    var user = new User
                    {
                        Id = 1,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = 1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        2
                    };

                    user = new User
                    {
                        Id = 2,
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = GenerateRandom.Email(),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    };

                    yield return new object[]
                    {
                        new List<Comment>
                        {
                            new Comment
                            {
                                Id = -1,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 1,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            },
                            new Comment
                            {
                                Id = 2,
                                Content = GenerateRandom.String(),
                                CreatedAt = GenerateRandom.DateTime(),
                                User = user,
                                Post = new Post
                                {
                                    Id = 2,
                                    User = user,
                                    CreatedAt = GenerateRandom.DateTime(),
                                    Content = GenerateRandom.String()
                                }
                            }
                        },
                        -1
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }
    }
}

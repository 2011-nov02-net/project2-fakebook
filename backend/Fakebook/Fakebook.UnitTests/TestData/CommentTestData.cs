using System;
using System.Collections.Generic;

using Fakebook.Domain;

namespace Fakebook.UnitTests.Repository.CommentTests
{
    public static class CommentTestData
    {
        public static class Create
        {
            /*
                public int Id { get; set; }
                public string Content { get; set; }
                public Post Post { get; set; }
                public Comment ParentComment { get; set; }
                public DateTime CreatedAt { get; set; }
                public User User { get; set; } 
             */

            public static IEnumerable<object[]> Valid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new User { }, new Post { }, new Comment { },  }
                    };
                }
            }

            public static IEnumerable<object[]> Invalid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }
        }

        public static class Read
        {
            public static IEnumerable<object[]> Valid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }

            public static IEnumerable<object[]> Invalid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }
        }

        public static class Update
        {
            public static IEnumerable<object[]> Valid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }

            public static IEnumerable<object[]> Invalid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }
        }

        public static class Delete
        {
            public static IEnumerable<object[]> Valid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }

            public static IEnumerable<object[]> Invalid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { new object(), new object() }
                    };
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

using Fakebook.Domain;

namespace Fakebook.UnitTests.TestData
{
    public static class UserTestData
    {
        public static class Create
        {
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

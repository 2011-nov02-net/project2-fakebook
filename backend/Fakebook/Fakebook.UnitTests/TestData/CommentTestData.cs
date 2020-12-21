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
            /*
                public int Id { get; set; } // generated
                public string Content { get; set; }
                public Post Post { get; set; }
                public Comment ParentComment { get; set; }
                public DateTime CreatedAt { get; set; }
                public User User { get; set; } 
             */
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Read
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Update
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Delete
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }
    }
}

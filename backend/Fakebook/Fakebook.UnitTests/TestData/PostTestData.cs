using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Domain;

namespace Fakebook.UnitTests.TestData
{
    public static class PostTestData
    {
        public static class Create
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
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
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
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
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
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
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[] { };
                    yield return new object[] { };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }
    }
}

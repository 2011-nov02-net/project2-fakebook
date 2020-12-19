using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Domain;

namespace Fakebook.UnitTests.TestData
{
    public static class UserTestData
    {
        public static class Create
        {
            /*
             * User:
             * - id: int
             * - profilePictureUrl: string?
             * - firstName: string
             * - lastname: string
             * - email: string
             * - phonenumber: string?
             * - birthdate: date
             * - status: string?
             */

            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  null,
                            LastName =  null,
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.String(),
                            PhoneNumber = GenerateRandom.String(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }

        public static class Read
        {
            public class Valid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        0
                    };

                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        GenerateRandom.Int(0, 2)
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        -1
                    };

                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        3
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
                    yield return new object[]
                    {
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        },
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        },
                        new User
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        },
                        new User
                        {
                            FirstName =  null,
                            LastName =  null,
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
                    };

                    yield return new object[]
                    {
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        },
                        new User
                        {
                            FirstName =  GenerateRandom.String(),
                            LastName =  GenerateRandom.String(),
                            ProfilePictureUrl = GenerateRandom.String(),
                            Email = GenerateRandom.String(),
                            PhoneNumber = GenerateRandom.String(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = GenerateRandom.String()
                        }
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
                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        0
                    };

                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        GenerateRandom.Int(0, 2)
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            public class Invalid : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator() {
                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        -1
                    };

                    yield return new object[]
                    {
                        new List<User>
                        {
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            },
                            new User
                            {
                                FirstName = GenerateRandom.String(),
                                LastName = GenerateRandom.String(),
                                ProfilePictureUrl = null,
                                Email = GenerateRandom.Email(),
                                PhoneNumber = GenerateRandom.PhoneNumber(),
                                BirthDate = GenerateRandom.DateTime(),
                                Status = null
                            }
                        },

                        3
                    };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }
    }
}

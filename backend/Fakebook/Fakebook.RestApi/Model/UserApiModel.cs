using System;
using System.Collections.Generic;

namespace Fakebook.RestApi.Model
{
    public class UserApiModel
    {
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }

        public List<int> FollowerIds { get; set; }
        public List<int> FolloweeIds { get; set; }
    }
}

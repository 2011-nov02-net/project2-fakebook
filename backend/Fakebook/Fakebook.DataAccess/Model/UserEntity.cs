using System;
using System.Collections.Generic;

namespace Fakebook.DataAccess.Model
{
    public class UserEntity
    {
        public UserEntity() {
            Followers = new HashSet<FollowEntity>();
            Followees = new HashSet<FollowEntity>();
            Posts = new HashSet<PostEntity>();
            Likes = new HashSet<LikeEntity>();
        }

        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<FollowEntity> Followers { get; set; }
        public virtual ICollection<FollowEntity> Followees { get; set; }
        public virtual ICollection<PostEntity> Posts { get; set; }
        public virtual ICollection<LikeEntity> Likes { get; set; }
    }
}

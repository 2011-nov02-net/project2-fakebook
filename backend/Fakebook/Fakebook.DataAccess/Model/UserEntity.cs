using System;
using System.Collections.Generic;

namespace Fakebook.DataAccess.Model
{
    public class UserEntity
    {
        public UserEntity() {
            Followers = new HashSet<UserEntity>();
            Followees = new HashSet<UserEntity>();
            Posts = new HashSet<PostEntity>();
            PostsLiked = new HashSet<PostEntity>();
        }

        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserEntity> Followers { get; set; }
        public virtual ICollection<UserEntity> Followees { get; set; }
        public virtual ICollection<PostEntity> Posts { get; set; }
        public virtual ICollection<PostEntity> PostsLiked { get; set; }
    }
}

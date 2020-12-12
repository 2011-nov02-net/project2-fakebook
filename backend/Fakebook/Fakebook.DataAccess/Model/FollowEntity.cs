using System;

namespace Fakebook.DataAccess.Model
{
    public class FollowEntity
    {
        public int FollowerId { get; set; }
        public UserEntity Follower { get; set; }
        public int FolloweeId { get; set; }
        public UserEntity Followee { get; set; }
    }
}

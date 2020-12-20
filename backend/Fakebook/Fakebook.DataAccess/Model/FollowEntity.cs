using System;

namespace Fakebook.DataAccess.Model
{
    public class FollowEntity
    {
        public FollowEntity() { }
        public FollowEntity(int id, int userId)
        {
            FollowerId = id;
            FolloweeId = userId;
        }
        public int FollowerId { get; set; }
        public UserEntity Follower { get; set; }
        public int FolloweeId { get; set; }
        public UserEntity Followee { get; set; }
    }
}

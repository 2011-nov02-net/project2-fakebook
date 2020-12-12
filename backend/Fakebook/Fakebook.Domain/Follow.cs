using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Domain
{
    public class Follow
    {
        public int FollowerId { get; set; }
        public User Follower { get; set; }
        public int FolloweeId { get; set; }
        public User Followee { get; set; }
    }
}

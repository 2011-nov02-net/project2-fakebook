using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.DataAccess.Model
{
    public class LikeEntity
    {
        public LikeEntity() { /*do nothing*/}
        public LikeEntity(int postId, int userId)
        {
            PostId = postId;
            UserId = userId;
        }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public virtual PostEntity Post { get; set; }
        public virtual UserEntity User { get; set; }
    }
}

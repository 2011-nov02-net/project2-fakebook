using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.DataAccess.Model
{
    public class PostEntity
    {
        public PostEntity()
        {
            Comments = new HashSet<CommentEntity>();
            Likes = new HashSet<LikeEntity>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<LikeEntity> Likes { get; set; }
    }
}

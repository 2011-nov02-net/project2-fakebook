using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Domain
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Domain
{
    public class Comment
    {
        public Comment()
        {
            ChildrenComments = new List<Comment>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public User User { get; set; } 
        public ICollection<Comment> ChildrenComments { get; set; }
    }
}

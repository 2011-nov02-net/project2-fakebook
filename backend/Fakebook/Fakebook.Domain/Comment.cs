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
        public string Content { get; set; }
        public Post Post { get; set; }
        public Comment ParentComment { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; } 

        public ICollection<Comment> ChildrenComments { get; set; }
    }
}

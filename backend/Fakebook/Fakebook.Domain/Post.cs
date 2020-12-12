using System;
using System.Collections.Generic;
using System.Text;
using Fakebook.DataAccess.Model;

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
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}

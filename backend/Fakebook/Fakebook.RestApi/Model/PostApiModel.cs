using System;
using System.Collections.Generic;

namespace Fakebook.RestApi.Model
{
    public class PostApiModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public List<int> LikedByUserIds { get; set; }
        public List<int> CommentIds { get; set; }
    }
}

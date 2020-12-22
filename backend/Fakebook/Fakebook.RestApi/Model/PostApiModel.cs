using System;
using System.Collections.Generic;

namespace Fakebook.RestApi.Model
{
    public class PostApiModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserApiModel User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> LikedByUserIds { get; set; }
        public List<int> CommentIds { get; set; }
    }
}

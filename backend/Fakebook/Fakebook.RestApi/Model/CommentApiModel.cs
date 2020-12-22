using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Domain;

namespace Fakebook.RestApi.Model
{
    public class CommentApiModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserApiModel User { get; set; }

        public List<int> ChildCommentIds { get; set; }
    }
}

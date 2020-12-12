using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.DataAccess.Model
{
    /// <summary>
    /// A comment to the Parent post. Will reference another comment entity.
    /// </summary>
    public class CommentEntity
    {
        /// <summary>
        /// A constructor for comment entity
        /// </summary>
        public CommentEntity() { /*Do nothing*/}

        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; } // references top author post
        public int ParentId { get; set; }  // references post that it is commenting to
        public DateTime CreatedAt { get; set; }
        public String Content { get; set; }

        public UserEntity User { get; set; } // connect to user

    }
}

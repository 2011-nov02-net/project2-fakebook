using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.DataAccess.Model
{
    public class LikeEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}

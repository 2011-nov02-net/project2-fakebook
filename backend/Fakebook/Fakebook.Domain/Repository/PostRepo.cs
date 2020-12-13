using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.DataAccess.Model;

namespace Fakebook.Domain.Repository
{
    public class PostRepo
    {
        private readonly FakebookContext _context;
        public PostRepo(FakebookContext context)
        {
            _context = context;
        }
        public async Task<List<PostEntity>> GetAllPostsAsync()
        {
            var posts = await _context.PostEntities.Select(e => DbEntityConverter.ToPost(e));
            return posts;
        }
    }
}

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
        public async Task<List<Post>> GetAllPostsAsync()
        {
            var entity = await _context.PostEntities.ToListAsync();
            var posts = entity.Select(e => DbEntityConverter.ToPost(e)).ToList();
            return posts;
        }
    }
}

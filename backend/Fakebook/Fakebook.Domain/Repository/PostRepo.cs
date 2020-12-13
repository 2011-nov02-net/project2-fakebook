using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.DataAccess.Model;

namespace Fakebook.Domain.Repository
{
    public class PostRepo : IPostRepo
    {
        private readonly FakebookContext _context;
        public PostRepo(FakebookContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            var entity = await _context.PostEntities.ToListAsync();
            var posts = entity.Select(e => DbEntityConverter.ToPost(e)).ToList();
            return posts;
        }
        public async Task<List<Post>> GetPostsById(int id)
        {
            var entity = await _context.PostEntities.Where(e => e.Id == id).ToListAsync();
            var posts = entity.Select(e => DbEntityConverter.ToPost(e)).ToList();
            return posts;
        }
        public async Task<List<Post>> GetPostsByUserId(int id)
        {
            var entity = await _context.PostEntities.Where(e => e.UserId == id).ToListAsync();
            var posts = entity.Select(e => DbEntityConverter.ToPost(e)).ToList();
            return posts;
        }
        public async Task<bool> CreatePost(Post post)
        {
            var newPost = DbEntityConverter.ToPostEntity(post);
            try
            {
                await _context.AddAsync(newPost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdatePost(Post post)
        {
            try
            {
                var entity = await _context.PostEntities.Where(e => e.Id == post.Id).FirstOrDefaultAsync();
                entity.Content = post.Content;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletePost(int id)
        {
            try
            {
                var entity = await _context.PostEntities.FindAsync(id);
                _context.PostEntities.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                Console.WriteLine("Invalid post");
                return false;
            }
        }
    }
}

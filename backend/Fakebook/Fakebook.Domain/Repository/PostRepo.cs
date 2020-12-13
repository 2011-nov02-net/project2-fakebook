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
        public async Task<Post> GetPostById(int id) {
            var posts = await _context.PostEntities
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ToListAsync();

            if(!posts.Any()) {
                return null;
            }

            var post = posts.FirstOrDefault(p => p.Id == id);

            return DbEntityConverter.ToPost(post);
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
        /// <summary>
        /// like a post
        /// </summary>
        /// <param name="PostId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<bool> LikePost(int PostId, int UserId)
        {
            try
            {
                var like = new LikeEntity(PostId, UserId); // convert
                await _context.AddAsync(like);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// Unlikes a post
        /// </summary>
        /// <param name="PostId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<bool> UnlikePost(int PostId, int UserId)
        {
            try
            {
                // find the like entity
                var entity = await _context.LikeEntities.FirstOrDefaultAsync(i => i.UserId == UserId & i.PostId == PostId); ;
                if (entity != null)
                {
                    // remove and if able to return true
                    _context.Remove(entity);
                    return true;
                }
                // otherwise return false
                return false;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// Count the amount of likes in a post and return it as an int.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CountLikes(int id)
        {
            var query = _context.LikeEntities.Where(c => c.PostId == id).Select(u => u.UserId).Count(); // selects the post by their id and counts the users
            return query;
        }

    }
}

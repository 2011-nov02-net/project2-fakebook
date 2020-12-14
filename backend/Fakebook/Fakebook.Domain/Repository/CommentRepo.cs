using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.DataAccess.Model;

using Fakebook.Domain.Extension;

using Microsoft.EntityFrameworkCore;

namespace Fakebook.Domain.Repository
{
    public class CommentRepo : ICommentRepo
    {
        private readonly FakebookContext _context;

        public CommentRepo(FakebookContext context) {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync() {
            //if(!await _context.CommentEntities.AnyAsync()) {
            //    return new List<Comment>();
            //}

            var entities = await _context.CommentEntities.Include(c => c.User).ToListAsync();
            var result = entities.Select(e => DbEntityConverter.ToComment(e)).ToList();
            return result;
        }

        public async Task<Comment> GetCommentByIdAsync(int id) {
            var item = await _context.CommentEntities.FindAsync(id);

            if(item is null) {
                return null;
            }

            return DbEntityConverter.ToComment(item);
        }

        public async Task<List<Comment>> GetCommentsByIdsAsync(ICollection<int> ids) {
            if(ids.Any()) {
                return new List<Comment>();
            }

            return await _context.CommentEntities
                .Where(c => ids.Contains(c.Id))
                .Select(c => DbEntityConverter.ToComment(c))
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByUserId(int userId) {
            if(!await _context.CommentEntities.AnyAsync() || await _context.UserEntities.FindAsync(userId) is null) {
                return new List<Comment>();
            }

            return await _context.CommentEntities
                .Where(c => c.UserId == userId)
                .Select(c => DbEntityConverter.ToComment(c))
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByPostId(int postId) {
            if (!await _context.CommentEntities.AnyAsync() || await _context.PostEntities.FindAsync(postId) is null) {
                return new List<Comment>();
            }

            return await _context.CommentEntities
                .Where(c => c.PostId == postId)
                .Select(c => DbEntityConverter.ToComment(c))
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(Comment comment) {
            try {
                comment.NullCheck(nameof(comment));

                var entity = DbEntityConverter.ToCommentEntity(comment);
                await _context.CommentEntities.AddAsync(entity);
                await _context.SaveChangesAsync();
            } catch {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(Comment comment) {
            try {
                comment.NullCheck(nameof(comment));

                var target = await _context.CommentEntities.FindAsync(comment.Id);

                if (target is null) {
                    return false;
                }

                var entity = DbEntityConverter.ToCommentEntity(comment);

                target.Content = entity.Content;
                target.CreatedAt = entity.CreatedAt;
                target.PostId = entity.PostId;
                target.UserId = entity.UserId;
                target.ParentId = entity.ParentId;

                await _context.SaveChangesAsync();
            } catch {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id) {
            var entity = await _context.CommentEntities.FindAsync(id);

            if (entity is null) {
                return false;
            }

            try {
                _context.CommentEntities.Remove(entity);
                await _context.SaveChangesAsync();
            } catch {
                return false;
            }

            return true;
        }
    }
}

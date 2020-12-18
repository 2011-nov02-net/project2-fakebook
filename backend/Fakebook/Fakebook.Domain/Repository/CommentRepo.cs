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
        public async Task<IEnumerable<Comment>> GetAllAsync() {
            var comments = await _context.CommentEntities
                .Include(c => c.Post)
                .Include(c => c.User)
                .Include(c => c.ChildrenComments)
                    .ThenInclude(c => c.Post)
                    .ThenInclude(c => c.User)
                .ToListAsync();

            if (!comments.Any()) {
                return new List<Comment>();
            }

            return comments
                .Select(c => DbEntityConverter.ToComment(c))
                .ToList();
        }

        public async Task<Comment> GetCommentByIdAsync(int userId) {
            var comments = await _context.CommentEntities
                .Include(c => c.Post)
                .Include(c => c.User)
                .Include(c => c.ChildrenComments)
                    .ThenInclude(c => c.Post)
                    .ThenInclude(c => c.User)
                .ToListAsync();

            var item = comments.FirstOrDefault(c => c.Id == userId);

            if (!comments.Any()) {
                return null;
            }

            return DbEntityConverter.ToComment(item);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByIdsAsync(ICollection<int> ids) {
            var comments = await _context.CommentEntities
                .Include(c => c.Post)
                .Include(c => c.User)
                .Include(c => c.ChildrenComments)
                    .ThenInclude(c => c.Post)
                    .ThenInclude(c => c.User)
                .ToListAsync();

            if (!ids.Any() || !comments.Any()) {
                return new List<Comment>();
            }

            return comments
                .Where(c => ids.Contains(c.Id))
                .Select(c => DbEntityConverter.ToComment(c));
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId) {
            var comments = _context.CommentEntities
                .Include(c => c.Post)
                .Include(c => c.User)
                .Include(c => c.ChildrenComments)
                    .ThenInclude(c => c.Post)
                    .ThenInclude(c => c.User);

            if (await _context.UserEntities.FindAsync(userId) is null || !comments.Any()) {
                return new List<Comment>();
            }

            return comments
                .Where(c => c.UserId == userId)
                .Select(c => DbEntityConverter.ToComment(c));
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId) {
            var comments = _context.CommentEntities
                .Include(c => c.Post)
                .Include(c => c.User)
                .Include(c => c.ChildrenComments)
                    .ThenInclude(c => c.Post)
                    .ThenInclude(c => c.User);

            if (await _context.PostEntities.FindAsync(postId) is null || !comments.Any()) {
                return new List<Comment>();
            }

            return comments
                .Where(c => c.PostId == postId)
                .Select(c => DbEntityConverter.ToComment(c));
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

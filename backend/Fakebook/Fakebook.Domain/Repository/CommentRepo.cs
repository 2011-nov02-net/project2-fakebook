using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.DataAccess.Model;

using Fakebook.Domain.Extension;

using Microsoft.EntityFrameworkCore;

namespace Fakebook.Domain.Repository
{
    /// <summary>
    /// Repository that manages comments within the application
    /// </summary>
    public class CommentRepo : ICommentRepo
    {
        private readonly FakebookContext _context;

        /// <summary>
        /// Creates a new instance of a CommentRepo
        /// </summary>
        /// <param name="context">The context in which the CommentRepo interacts with</param>
        public CommentRepo(FakebookContext context) {
            _context = context;
        }

        /// <summary>
        /// Creates a new comment with the given comment data.
        /// </summary>
        /// <param name="comment">The object containing the comment data</param>
        /// <returns>A boolean as to whether the creation succeeded or failed</returns>
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

        /// <summary>
        /// Retrieves all of the Comments as an IEnumerable.
        /// </summary>
        /// <returns>A collection of all Comment objects</returns>
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

        /// <summary>
        /// Gets a Comment by its id.
        /// </summary>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>A 'Comment' object, if it is found, or else it returns null</returns>
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

        /// <summary>
        /// Gets a collection of 'Comment' object by a collection of ids
        /// </summary>
        /// <param name="ids">A set of comment ids</param>
        /// <returns>A collection of comments that have any of the ids passed in</returns>
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

        /// <summary>        
        /// /// Gets the comments associated with a user's id
        /// </summary>
        /// <param name="userId">The id of the user that posted the comments</param>
        /// <returns>A collection of 'Comment' objects by a user's id</returns>
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

        /// <summary>
        /// Get the comments associated with a post
        /// </summary>
        /// <param name="postId">The id of the post</param>
        /// <returns>A collection of 'Comment' objects that are associated with</returns>
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

        /// <summary>
        /// Updates a comment with the given data.
        /// If it is not found, the method returns false.
        /// </summary>
        /// <param name="comment">The data of the 'Comment' object to update</param>
        /// <returns>True if the 'Comment' object was found and updated, false if it was not found or an issue occurred</returns>
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

        /// <summary>
        /// Deletes a 'Comment' object by its id.
        /// </summary>
        /// <param name="id">The id of the 'Comment' object</param>
        /// <returns>True if the object was found and deleted, false if it wasn't found or an exception occurred</returns>
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

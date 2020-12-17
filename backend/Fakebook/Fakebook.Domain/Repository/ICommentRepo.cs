using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    /// <summary>
    /// An interface that exposes a contract to perform actions on a given set of 
    /// a collection of 'Comment' objects.
    /// </summary>
    public interface ICommentRepo
    {
        /// <summary>
        /// Creates a new comment with the given comment data.
        /// </summary>
        /// <param name="comment">The object containing the comment data</param>
        /// <returns>A boolean as to whether the creation succeeded or failed</returns>
        Task<bool> CreateAsync(Comment comment);

        /// <summary>
        /// Retrieves all of the Comments as an IEnumerable.
        /// </summary>
        /// <returns>A collection of all Comment objects</returns>
        Task<IEnumerable<Comment>> GetAllAsync();

        /// <summary>
        /// Gets a Comment by its id.
        /// </summary>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>A 'Comment' object, if it is found, or else it returns null</returns>
        Task<Comment> GetCommentByIdAsync(int commentId);

        /// <summary>
        /// Gets a collection of 'Comment' object by a collection of ids
        /// </summary>
        /// <param name="ids">A set of comment ids</param>
        /// <returns>A collection of comments that have any of the ids passed in</returns>
        Task<IEnumerable<Comment>> GetCommentsByIdsAsync(ICollection<int> ids);

        /// <summary>        
        /// /// Gets the comments associated with a user's id
        /// </summary>
        /// <param name="userId">The id of the user that posted the comments</param>
        /// <returns>A collection of 'Comment' objects by a user's id</returns>
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);

        /// <summary>
        /// Get the comments associated with a post
        /// </summary>
        /// <param name="postId">The id of the post</param>
        /// <returns>A collection of 'Comment' objects that are associated with</returns>
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

        /// <summary>
        /// Updates a comment with the given data.
        /// If it is not found, the method returns false.
        /// </summary>
        /// <param name="comment">The data of the 'Comment' object to update</param>
        /// <returns>True if the 'Comment' object was found and updated, false if it was not found or an issue occurred</returns>
        Task<bool> UpdateAsync(Comment comment);

        /// <summary>
        /// Deletes a 'Comment' object by its id.
        /// </summary>
        /// <param name="id">The id of the 'Comment' object</param>
        /// <returns>True if the object was found and deleted, false if it wasn't found or an exception occurred</returns>
        Task<bool> DeleteAsync(int id);

    }
}
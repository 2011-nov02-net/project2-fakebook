using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface ICommentRepo
    {
        Task<bool> CreateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetCommentByIdAsync(int userId);
        Task<IEnumerable<Comment>> GetCommentsByIdsAsync(ICollection<int> ids);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<bool> UpdateAsync(Comment comment);
    }
}
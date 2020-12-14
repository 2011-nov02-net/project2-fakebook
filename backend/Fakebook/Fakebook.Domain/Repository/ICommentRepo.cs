using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface ICommentRepo
    {
        Task<bool> CreateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByIdsAsync(ICollection<int> ids);
        Task<bool> UpdateAsync(Comment comment);
    }
}
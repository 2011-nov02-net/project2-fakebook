using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IUserRepo
    {
        Task<bool> CreateUser(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> FollowUserAsync(int id, int userId);
        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<int> ids);
        Task<bool> UnfollowUserAsync(int id, int userId);
        Task<bool> UpdateUserAsync(int id, User user);
    }
}
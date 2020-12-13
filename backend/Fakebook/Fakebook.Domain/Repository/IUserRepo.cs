using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IUserRepo
    {
        Task<bool> CreateUser(User user);
        Task<bool> DeleteUser(int id);
        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<bool> UpdateUser(User user);
    }
}
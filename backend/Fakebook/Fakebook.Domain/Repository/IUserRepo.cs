using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
    }
}
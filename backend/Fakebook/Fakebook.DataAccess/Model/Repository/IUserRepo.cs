using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.DataAccess.Model.Repository
{
    public interface IUserRepo
    {
        List<UserEntity> GetAllUsers();
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUserByEmail(string email);
        Task<UserEntity> GetUserById(int id);
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.DataAccess.Model.Repository
{

    public class UserRepo : IUserRepo
    {
        // create a read only for our database
        private readonly FakebookContext _context;
        public UserRepo(FakebookContext context)
        {
            _context = context;
        }
        public List<UserEntity> GetAllUsers()
        {
            var entity = _context.UserEntities.ToList();
            return entity;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var entity = await _context.UserEntities.ToListAsync();
            return entity;
        }
        public async Task<List<UserEntity>> GetUserById(int id)
        {
            var entity = await _context.UserEntities.Where(e => e.Id == id).FirstOrDefaultAsync();
            return entity;
        }
        public async Task<List<UserEntity>> GetUserByEmail(string email)
        {
            var entity = await _context.UserEntities.Where(e => e.Email == email).FirstOrDefaultAsync();
            return entity;
        }

    }
}

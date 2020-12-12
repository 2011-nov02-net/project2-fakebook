using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.DataAccess.Model.Repository
{

    public class UserRepository
    {
        // create a read only for our database
        private readonly FakebookContext _context;
        public UserRepository(FakebookContext context)
        {
            _context = context;
        }
        public List<UserEntity> GetAllUsers()
        {
            var entity = _context.UserEntities.ToList();
            return entity;
        }

        public async Task <List<UserEntity>> GetAllUsersAsync()
        {
            var entity = await _context.UserEntities.ToListAsync();
            return entity;
        }

    }
}

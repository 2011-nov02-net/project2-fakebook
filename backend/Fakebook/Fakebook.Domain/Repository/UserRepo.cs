﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.DataAccess.Model;

namespace Fakebook.Domain.Repository
{

    public class UserRepo : IUserRepo
    {
        // create a read only for our database
        private readonly FakebookContext _context;
        public UserRepo(FakebookContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            var entity = _context.UserEntities.ToList();
            return entity;
        }
        /// <summary>
        /// Get all users asyncronously
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            var entity = await _context.UserEntities.ToListAsync();
            return entity;
        }
        /// <summary>
        /// Get user by the id that they pass into the conroller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            var entity = await _context.UserEntities.FindAsync(id);
            return entity;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var entity = await _context.UserEntities.Where(e => e.Email == email).FirstOrDefaultAsync();
            return entity;
        }
    }
}
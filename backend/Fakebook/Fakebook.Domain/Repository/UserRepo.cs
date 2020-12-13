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
        public IEnumerable<User> GetAllUsers()
        {
            var entity = _context.UserEntities.ToList();
            var users = entity.Select(e => DbEntityConverter.ToUser(e));
            return users;
        }
        /// <summary>
        /// Get all users asyncronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var entities = await _context.UserEntities.ToListAsync();
            var users = entities.Select(e => DbEntityConverter.ToUser(e)); // turn into a list.
            return users;
        }
        /// <summary>
        /// Get user by the id that they pass into the conroller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            var entity = await _context.UserEntities.FindAsync(id);
            var user = DbEntityConverter.ToUser(entity); // turn into a user
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var entity = await _context.UserEntities.Where(e => e.Email == email).FirstOrDefaultAsync();
            var user = DbEntityConverter.ToUser(entity); // turn into a user
            return user;
        }
        /// <summary>
        /// try to create a user if it goes through return a true otherwise return a flase
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CreateUser(User user)
        {
            var newUser = DbEntityConverter.ToUserEntity(user); // convert
            try
            {
                await _context.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// Find a user then delete them. Added a catch block in case it didn't wrok
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {

                var entity = await _context.UserEntities.FindAsync(id);
                _context.UserEntities.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                Console.WriteLine("Invalid user");
                return false;
            }

        }
        /// <summary>
        /// convert to user entity then save changes.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(int id, User user)
        {
            try
            {
                UserEntity entity = await _context.UserEntities.FindAsync(id);
                // assign all the values
                {
                    entity.Status = user.Status;
                    entity.FirstName = user.FirstName;
                    if (!String.IsNullOrEmpty(user.Email))
                        entity.Email = user.Email;
                    entity.LastName = user.LastName;
                }
                // save changes.
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
using Microsoft.EntityFrameworkCore;
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
            var entity = _context.UserEntities
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .Include(u => u.Posts)
                .ToList();
            var users = entity.Select(e => DbEntityConverter.ToUser(e));
            return users;
        }
        /// <summary>
        /// Get all users asyncronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var entities = await _context.UserEntities
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .Include(u => u.Posts)
                .ToListAsync();
            var users = entities.Select(e => DbEntityConverter.ToUser(e)); // turn into a list.
            return users;
        }

        /// <summary>
        /// Get a collection of users from a collection of their ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
        {
            var users = await _context.UserEntities
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .Include(u => u.Posts)
                .ToListAsync();

            if (!ids.Any() || !users.Any())
            {
                return new List<User>();
            }

            return users
                .Where(u => ids.Contains(u.Id))
                .Select(u => DbEntityConverter.ToUser(u))
                .ToList();
        }

        /// <summary>
        /// Get user by the id that they pass into the conroller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var entity = await _context.UserEntities
                .Where(u => u.Id == id)
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .FirstOrDefaultAsync();
            var user = DbEntityConverter.ToUser(entity); // turn inato a user
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var entity = await _context.UserEntities
                .Where(e => e.Email == email)
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .Include(u => u.Posts)
                .FirstOrDefaultAsync();
            var user = DbEntityConverter.ToUser(entity); // turn into a user
            return user;
        }
        /// <summary>
        /// get's users by a string that contains anything in this string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUserByName(string name)
        {
            // loser the string of both name and FirstName. also works for last name
            var entity = await _context.UserEntities.Where(n => n.FirstName.ToLower().Contains(name.ToLower()) || n.LastName.ToLower().Contains(name.ToLower())).ToListAsync();

            var users = entity.Select(e => DbEntityConverter.ToUser(e)); // turn into a list.
            return users;

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
        public async Task<bool> DeleteUserAsync(int id)
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
        public async Task<bool> UpdateUserAsync(int id, User user)
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
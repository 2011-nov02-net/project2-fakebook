using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.DataAccess.Model;
using Fakebook.Domain.Extension;

namespace Fakebook.Domain.Repository
{

    public class UserRepo : IUserRepo
    {
        // create a read only for our database
        private readonly FakebookContext _context;
        public UserRepo(FakebookContext context) {
            _context = context;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers() {
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
        public async Task<IEnumerable<User>> GetAllUsersAsync() {
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
        public async Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<int> ids) {
            var userEntities = _context.UserEntities;
            var userIds = userEntities
                .Select(u => u.Id)
                .ToList();

            if (!ids.All(id => userIds.Contains(id))) {
                throw new ArgumentException("Not all ids requested are present.");
            }

            var users = await userEntities
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .Include(u => u.Posts)
                .ToListAsync();

            if (!ids.Any() || !users.Any()) {
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
        public async Task<User> GetUserByIdAsync(int id) {
            var entities = _context.UserEntities;

            if (id < 1 || !entities.Any() || id > entities.Max(u => u.Id)) {
                throw new ArgumentException($"{id} is not a valid id.");
            }

            var entity = await entities
                .Where(u => u.Id == id)
                .Include(u => u.Followees)
                     .ThenInclude(u => u.Followee)
                .Include(u => u.Followers)
                      .ThenInclude(u => u.Follower)
                .FirstOrDefaultAsync();
            var user = DbEntityConverter.ToUser(entity); // turn inato a user
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email) {
            var entities = _context.UserEntities;

            email.EnforceEmailCharacters(nameof(email));

            if (!entities.Any()) {
                throw new ArgumentException($"{email} does not belong to any user");
            }

            var entity = await entities
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
        public async Task<IEnumerable<User>> GetUserByName(string name) {
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
        public async Task<int> CreateUser(User user) {
            try {
                var newUser = DbEntityConverter.ToUserEntity(user); // convert
                await _context.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return newUser.Id;
            } catch {
                return -1;
            }

        }
        /// <summary>
        /// Find a user then delete them. Added a catch block in case it didn't wrok
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(int id) {
            var entities = _context.UserEntities;

            if (id < 1 || !entities.Any() || id > entities.Max(u => u.Id)) {
                throw new ArgumentException($"{id} is not a valid id.");
            }

            var entity = await entities.FindAsync(id);
            _context.UserEntities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// convert to user entity then save changes.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAsync(int id, User user) {
            // make sure the user can be converted
            var userEntity = DbEntityConverter.ToUserEntity(user);

            var entities = _context.UserEntities;

            if (id < 1 || !entities.Any() || id > entities.Max(u => u.Id)) {
                throw new ArgumentException($"{id} is not a valid id.");
            }

            UserEntity entity = await entities.FindAsync(id);
            // assign all the values
            {
                entity.Status = userEntity.Status;
                entity.FirstName = userEntity.FirstName;
                if (!userEntity.Email.IsNullOrEmpty())
                    entity.Email = userEntity.Email;
                entity.LastName = userEntity.LastName;
            }
            // save changes.
            _context.SaveChanges();
            return true;
        }

        // id is the follower, userId is followee aka person that the follower is following
        public async Task<bool> FollowUserAsync(int id, int userId) {
            if(id == userId) {
                return false;
            }

            var follow = new FollowEntity(userId, id); // convert
            await _context.FollowEntities.AddAsync(follow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnfollowUserAsync(int id, int userId) {
            if(id == userId) {
                return false;
            }

            // find the like entity
            var entity = await _context.FollowEntities.FirstOrDefaultAsync(i => i.FollowerId == id && i.FolloweeId == userId);
            if (entity != null) {
                // remove and if able to return true
                _context.FollowEntities.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            // otherwise return false
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Repositories;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly UserContext _db;

        public UserRepository(UserContext db)
        {
            _db = db;
        }

        // Retrieve all users from the database
        public async Task<IEnumerable<Core.Entities.User>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }

        // Retrieve only active user (isActive == true)
        public async Task<IEnumerable<Core.Entities.User>> GetAllActiveUsers()
        {
            return await _db.Users.Where(u => u.isActive).ToListAsync();
        }

        // Retrieve only inactive user (isActive == false)
        public async Task<IEnumerable<Core.Entities.User>> GetAllInactiveUsers()
        {
            return await _db.Users.Where(u => !u.isActive).ToListAsync();
        }

        // Retrieve the user by ID(int)
        public async Task<Core.Entities.User> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }

        // Retrieve the user by Email
        public async Task<Core.Entities.User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Create a new user
        public async Task<Core.Entities.User> CreateUser(Core.Entities.User user)
        {
            var entity = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return entity.Entity;
        }

        // Update an existing user
        public async Task<Core.Entities.User> UpdateUser(Core.Entities.User user)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null) throw new Exception("User not found!");
            // Update only the modified properties
            _db.Entry(existingUser).CurrentValues.SetValues(user);
            await _db.SaveChangesAsync();
            return user;
        }

        // Soft delete a user by setting isActive to false
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return false;

            //_db.Users.Remove(user);
            user.isActive = false;
            await _db.SaveChangesAsync();
            return true;
        }

        // Restore the user
        public async Task<bool> RestoreUser(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            user.isActive = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

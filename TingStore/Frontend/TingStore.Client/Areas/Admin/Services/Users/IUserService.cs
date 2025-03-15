using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Users;

namespace TingStore.Client.Areas.Admin.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<IEnumerable<UserResponse>> GetAllActiveUsers();
        Task<IEnumerable<UserResponse>> GetAllInactiveUsers();
        Task<UserResponse> GetUserById(int id);
        Task<UserResponse> GetUserByEmailAsync(string email);
        Task<UserResponse> CreateUser(CreateUserRequest user);
        Task<bool> UpdateUser(UpdateUserRequest user);
        Task<bool> DeleteUser(int id);
        Task<bool> RestoreUser(int id);
    }
}

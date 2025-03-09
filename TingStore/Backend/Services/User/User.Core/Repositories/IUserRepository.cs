using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Core.Entities.User>> GetAllUsers();
        Task<IEnumerable<Core.Entities.User>> GetAllActiveUsers();
        Task<IEnumerable<Core.Entities.User>> GetAllInactiveUsers();
        Task<Core.Entities.User> GetUserById(int id);
        Task<Core.Entities.User> GetUserByEmail(string email);
        Task<Core.Entities.User> CreateUser(Core.Entities.User user);
        Task<Core.Entities.User> UpdateUser(Core.Entities.User user);
        Task<bool> DeleteUser(int id);
        Task<bool> RestoreUser(int id);
    }
}

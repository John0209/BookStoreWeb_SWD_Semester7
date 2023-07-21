using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IUserService
    {
        Task<bool> CreateUserMoble(User user);
        Task<bool> CreateUserFE(User user);
        Task<bool>RecoverPassword(string email);
        Task<User> CheckLogin(LoginDTO login);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(Guid userId);
        Task<IEnumerable<User>> GetUserByName(string name);
        Task<bool> UpdateUser(User user);
        Task<bool> UpdateRole(Guid userId, int roleId);
        Task<bool> RemoveUser(Guid userId);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> RestoreUser(Guid userId);
    }
}

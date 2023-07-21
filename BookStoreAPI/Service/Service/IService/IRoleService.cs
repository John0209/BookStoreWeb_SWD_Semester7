using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IRoleService
    {
        Task<bool> CreateRole(Role role);
        Task<IEnumerable<Role>> GetAllRole();
        Task<Book> GetRoleById(string roleId);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(string roleId);
    }
}

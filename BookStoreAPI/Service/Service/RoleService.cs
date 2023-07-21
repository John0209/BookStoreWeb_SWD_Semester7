using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RoleService : IRoleService
    {
        IUnitOfWorkRepository _unit;
        public RoleService(IUnitOfWorkRepository unit)
        {
            _unit = unit;
        }

        public Task<bool> CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            var role = await _unit.Role.GetAll();
            if (role.Count() > 0) return role;
            return null;
        }

        public Task<Book> GetRoleById(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}

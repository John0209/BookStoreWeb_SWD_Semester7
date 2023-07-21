using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}

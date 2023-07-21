using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Interface
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        IBookingRequestRepository Request { get; }
        IBookRepository Books { get; }
        ICategoryRepository Category { get; }
        IImageBookRepository Images { get; }
        IImportationRepository Importation { get; }
        IImportationDetailRepository ImportationDetail { get; }
        IInventoryRepository Inventory { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        int Save();
        Task<int> SaveList();
    }
}

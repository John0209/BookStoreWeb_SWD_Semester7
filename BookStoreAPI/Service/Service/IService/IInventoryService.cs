using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IInventoryService
    {
        Task<bool> CreateInventory(Inventory inventory);
        Task<IEnumerable<DisplayInventoryDTO>> GetAllInventory();
        Task<IEnumerable<Inventory>> GetInventory();
        Task<List<DisplayInventoryDTO>> SearchInventory(string bookName);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> DeleteInventory(Guid inventoryId);
        Task<bool> RestoreInventory(Guid inventoryId);
        Task<bool> RemoveInventory(Guid inventoryId);
    }
}

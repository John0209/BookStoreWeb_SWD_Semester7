using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Order>> GetOrderByUserId(Guid userId);
        Task<Order> GetOrderByOrderId(Guid orderId);
        Task<bool> RemoveOrder(Guid orderId);
        Task<Order> SearchByOrderCode(string orderCode);
        // 0: deleted, 1: processing, 2: done, 3: undone, 4: just created, 5: confirm
        Task<bool> CreateOrder(Order order);//status 4
        Task<bool> DeleteOrder(Guid orderId);//status 0
        Task<bool> RestoreOrder(Guid orderId);// back 2
        Task<bool> OrderSuccess(Guid orderId);//status 2
        Task<bool> OrderFail(Guid orderId);//status 3
        Task<Guid> GetOrderIdJustCreated();//status 5
        Task<bool> ConfirmOrder(Guid orderId);//status 1
    }
}

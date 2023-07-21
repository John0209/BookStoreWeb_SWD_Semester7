using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IOrderDetailService
    {
        Task<bool> CreateOrderDetail(OrderDetail orderDetail);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetail();
        Task<List<DisplayOrderDetailDTO>> SearchOrder(string bookName);
        Task<IEnumerable<DisplayOrderDetailDTO>> GetDisplayOrderDetail();
        Task<bool> UpdateOrderDetail(OrderDetail orderDetail);
        Task<IEnumerable<DisplayOrderDetailDTO>> GetOrderDetailByOrderId(Guid order_id);

    }
}

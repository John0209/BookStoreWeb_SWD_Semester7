using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class OrderService : IOrderService
    {
        IUnitOfWorkRepository _unit;
        IUserService _user;
        private readonly Order m_order;
        // 0: deleted, 1: processing, 2: done, 3: undone, 4: just created
        public OrderService(IUnitOfWorkRepository unit, IUserService user)
        {
            _unit = unit;
            _user = user;
        }
        public async Task<bool> CreateOrder(Order order)
        {
            if (order != null)
            {
                order.Order_Id = Guid.NewGuid();
                order.Order_Code = CreateCodeOrder();
                order.Order_Customer_Name =await TakeName(order.User_Id, order.Order_Customer_Name);
                order.Order_Customer_Address = await TakeAddress(order.User_Id, order.Order_Customer_Address);
                order.Order_Customer_Phone = await TakePhone(order.User_Id, order.Order_Customer_Phone);
                order.Is_Order_Status = 4;
                await _unit.Order.Add(order);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        private string CreateCodeOrder()
        {
            Random rd = new Random();
           int number= rd.Next(1000, 100000);
            return "O" + number;
        }

        private async Task<string> TakeName(Guid user_Id, string order_Customer_Name)
        {
            if (order_Customer_Name.Equals("string"))
            {
                var user= await _unit.User.GetById(user_Id);
                if(user != null)
                {
                    order_Customer_Name = user.User_Name;
                }
            }
            return order_Customer_Name;
        }
        private async Task<string> TakeAddress(Guid user_Id, string order_Customer_Name)
        {
            if (order_Customer_Name.Equals("string"))
            {
                var user = await _unit.User.GetById(user_Id);
                if (user != null)
                {
                    order_Customer_Name = user.User_Address;
                }
            }
            return order_Customer_Name;
        }
        private async Task<string> TakePhone(Guid user_Id, string order_Customer_Name)
        {
            if (order_Customer_Name.Equals("string"))
            {
                var user = await _unit.User.GetById(user_Id);
                if (user != null)
                {
                    order_Customer_Name = user.User_Phone;
                }
            }
            return order_Customer_Name;
        }
        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var m_update = _unit.Order.SingleOrDefault(m_order, u => u.Order_Id == orderId);
            if (m_update != null)
            {
                m_update.Is_Order_Status = 0;
                _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            var result = await _unit.Order.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

      
        private async Task<bool> UpdateStatusOrder(Guid orderID)
        {
            var m_update = _unit.Order.SingleOrDefault(m_order, u => u.Order_Id==orderID);
            if (m_update!= null)
            {
                m_update.Is_Order_Status = 5;
                _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreOrder(Guid orderId)
        {
            var m_update = _unit.Order.SingleOrDefault(m_order, u => u.Order_Id == orderId);
            if (m_update != null)
            {
                m_update.Is_Order_Status = 2;
                _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(Guid userId)
        {
            var listOrder = await GetAllOrder();
            var order = from b in listOrder where b.User_Id == userId select b;
            if (order != null)
            return order;
            return null;
        }

        public async Task<bool> RemoveOrder(Guid orderId)
        {
            var import = await _unit.Order.GetById(orderId);
            var importDetailList = await _unit.OrderDetail.GetAll();
            var listDetail = from i in importDetailList where i.Order_Id == orderId select i;
            if (import != null)
            {
                if (listDetail.Count() > 0)
                {
                    foreach (var i in listDetail)
                    {
                        _unit.OrderDetail.Delete(i);
                        _unit.Save();
                    }
                }
                _unit.Order.Delete(import);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<Order> GetOrderByOrderId(Guid orderId)
        {
            var order = await _unit.Order.GetById(orderId);
            if(order != null)
            {
                return order;
            }
            return null;
        }

        public async Task<Guid> GetOrderIdJustCreated()
        {
            var listOrder = await _unit.Order.GetAll();
            var orderId = (from i in listOrder where i.Is_Order_Status == 4 select i.Order_Id).FirstOrDefault();
            // sau khi lấy đước rồi thì update lại status import
            await UpdateStatusOrder(orderId);
            return orderId;
        }

        public async Task<bool> ConfirmOrder(Guid orderId)
        {
            var m_update = await _unit.Order.GetById(orderId);
            if (m_update != null)
            {
                m_update.Is_Order_Status = 1;
                 _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> OrderSuccess(Guid orderId)
        {
            var m_update = await _unit.Order.GetById(orderId);
            if (m_update != null)
            {
                m_update.Is_Order_Status = 2;
                _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> OrderFail(Guid orderId)
        {
            var m_update = await _unit.Order.GetById(orderId);
            if (m_update != null)
            {
                m_update.Is_Order_Status = 3;
                _unit.Order.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<Order> SearchByOrderCode(string orderCode)
        {
            var listOrder = await _unit.Order.GetAll();
            var order= (from o in listOrder where o.Order_Code.Equals(orderCode) select o).FirstOrDefault();
            if(order == null) return null;
            return order;
        }
    }
}

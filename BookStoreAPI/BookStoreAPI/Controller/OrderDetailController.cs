using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.IService;
using static System.Net.Mime.MediaTypeNames;

namespace BookStoreAPI.Controller
{
    [Route("api/orderDetail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        IOrderDetailService _order;
        IMapper _map;
        public OrderDetailController(IOrderDetailService order, IMapper mapper)
        {
            _map = mapper;
            _order = order;
        }
        [HttpGet("searchOrder")]
        public async Task<IActionResult> SearchOrder(string bookName)
        {
            var respone = await _order.SearchOrder(bookName);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest(bookName + " don't exists");
        }
        [HttpGet("getOrderDetail")]
        public async Task<IActionResult> GetOrderDetail()
        {
            var respone = await _order.GetDisplayOrderDetail();
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("order detail don't exists");
        }
        [HttpGet("getByOrderId")]
        public async Task<IActionResult> GetByOrderId(Guid Order_id)
        {
            var respone = await _order.GetOrderDetailByOrderId(Order_id);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("order detail don't exists");
        }
        [HttpPost("createOrderDetail")]
        public async Task<IActionResult> CreateOrderDetail(OrderDetailDTO dto)
        {
            if (dto != null)
            {
                var order = _map.Map<OrderDetail>(dto);
                var result = await _order.CreateOrderDetail(order);
                if (result) return Ok("Add Order Detail Success");
            }
            return BadRequest("Add Order Detail Fail");
        }
    }
}

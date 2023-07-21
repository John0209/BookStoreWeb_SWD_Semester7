using BookStoreAPI.Core.DiplayDTO;
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
    public class OrderDetailService : IOrderDetailService
    {
        IUnitOfWorkRepository _unit;
        IBookService _book;
        private readonly OrderDetail m_order;
        public OrderDetailService(IUnitOfWorkRepository unit, IBookService book)
        {
            _unit = unit;
            _book = book;
        }
        public async Task<bool> CreateOrderDetail(OrderDetail order)
        {
            if (order != null)
            {
               // var m_list = await GetAllOrderDetail();
                order.Order_Detail_Id = Guid.NewGuid();

                await UpdateBookQuantity(order.Book_Id, order.Order_Detail_Quantity);
                await _unit.OrderDetail.Add(order);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        private async Task UpdateBookQuantity(Guid book_Id, int order_Detail_Quantity)
        {
            var book = await _unit.Books.GetById(book_Id);
            if (book != null)
            {
                book.Book_Quantity -= order_Detail_Quantity;
                _unit.Books.Update(book);
                _unit.Save();
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail()
        {
            var result = await _unit.OrderDetail.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<DisplayOrderDetailDTO>> GetDisplayOrderDetail()
        {
            var orderList = await _unit.OrderDetail.GetAll();
            var display = new List<DisplayOrderDetailDTO>();
            // get filed để display
            display = await GetDisplay(display, orderList);
            if (display.Count < 1) return null;
            return display;

        }

        private async Task<List<DisplayOrderDetailDTO>> GetDisplay(List<DisplayOrderDetailDTO> display, IEnumerable<OrderDetail> orderList)
        {
            var bookList = await _unit.Books.GetAll();
            var image = await _unit.Images.GetAll();
            foreach (var item in orderList)
            {
                var order = new DisplayOrderDetailDTO();
                order.Order_Id = item.Order_Id;
                order.Order_Detail_Quantity = item.Order_Detail_Quantity;
                order.Order_Detail_Price = item.Order_Detail_Price;
                order.Order_Detail_Amount = item.Order_Detail_Amount;
                order.Book_Title = GetTitle(item.Book_Id, bookList);
                order.Image_URL = GetUrl(item.Book_Id, image);
                display.Add(order);
            }
            return display;
        }

        private string GetUrl(Guid book_Id, IEnumerable<ImageBook> image)
        {
            var url = (from b in image where b.Book_Id == book_Id select b.Image_URL).FirstOrDefault();
            return url;
        }

        private string GetTitle(Guid book_Id, IEnumerable<Book> bookList)
        {
            var title = (from b in bookList where b.Book_Id == book_Id select b.Book_Title).FirstOrDefault();
            return title;
        }
        public async Task<List<DisplayOrderDetailDTO>> SearchOrder(string bookName)
        {
            var books = await _unit.Books.GetAll();
            var orders = await GetAllOrderDetail();
            // lấy nhựng book có name cẩn search
            var bookIdList = from b in books where (b.Book_Title.ToLower().Trim().Contains(bookName.ToLower().Trim())) select b;
            // lấy inventory có chứa những book có id cần search
            var orderList = (bookIdList.Join(orders, b => b.Book_Id, i => i.Book_Id, (b, i) => { return i; }));
            //lấy thông tin để show ra screen
            var listDisplay = new List<DisplayOrderDetailDTO>();
            listDisplay = await GetDisplay(listDisplay, orderList);
            if (listDisplay.Count() > 0) return listDisplay;
            return null;
        }

        public Task<bool> UpdateOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DisplayOrderDetailDTO>> GetOrderDetailByOrderId(Guid order_id)
        {
            var listOrderDetail = await GetAllOrderDetail();
            var oderDetail = from i in listOrderDetail where i.Order_Id== order_id select i;
            var display = new List<DisplayOrderDetailDTO>();
            // get filed để display
            display = await GetDisplay(display, oderDetail);
            if (display.Count < 1) return null;
            return display;
        }
    }
}

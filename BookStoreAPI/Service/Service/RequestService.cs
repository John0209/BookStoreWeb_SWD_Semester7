using Azure.Core;
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
    public class RequestService : IRequestService
    {
        IUnitOfWorkRepository _unit;
        private BookingRequest m_request;
        public RequestService(IUnitOfWorkRepository unit)
        { 
            _unit = unit;
        }

        public async Task<bool> CreateRequest(BookingRequest request, bool status)
        {
            switch (status)
            {
                    //Book New
                case true:
                    if (request != null)
                    {
                        //var m_list = await GetAllRequest();
                        request.Request_Id = Guid.NewGuid();
                        //nếu là book cũ, sẽ trả lại id đã truyền xuống
                        request.Book_Id = await GetBookId(request.Book_Id);
                        request.Request_Date = DateTime.Now;
                        request.Is_Request_Status = 1;
                        await _unit.Request.Add(request);
                        var result = _unit.Save();
                        if (result > 0) return true;
                    }
                    return false;
                    //Book Exists
                case false:
                    if (request != null)
                    {
                        //var m_list = await GetAllRequest();
                        request.Request_Id = Guid.NewGuid();
                        //nếu là book cũ, sẽ trả lại id đã truyền xuống
                        request.Book_Id = await GetBookId(request.Book_Id);
                        var book = await _unit.Books.GetById(request.Book_Id);
                        request.Request_Book_Name = book.Book_Title;
                        request.Request_Price = book.Book_Price;
                        request.Request_Date = DateTime.Now;
                        //get image
                        var listImage = await _unit.Images.GetAll();
                        request.Request_Image_Url = GetUrl(listImage, book.Book_Id);
                        request.Is_Request_Status = 1;
                        await _unit.Request.Add(request);
                        var result = _unit.Save();
                        if (result > 0) return true;
                    }
                    return false;
            }
            
        }
        private string GetUrl(IEnumerable<ImageBook> listImage, Guid book_Id)
        {
            var url = (from i in listImage where i.Book_Id == book_Id select i.Image_URL).FirstOrDefault();
            return url;
        }
        private async Task<Guid> GetBookId(Guid book_Id)
        {
           var book=await _unit.Books.GetById(book_Id);
            // nếu book id k có chứng tỏ là book mới, gắn tạm book id đã có
            if (book == null)
            {
                var bookList = await _unit.Books.GetAll();
                var book_Id_Exists = bookList.FirstOrDefault().Book_Id;
                return book_Id_Exists;
            }
            return book_Id;
        }

        public async Task<IEnumerable<BookingRequest>> GetAllRequest()
        {
            var result = await _unit.Request.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<BookingRequest> GetRequestById(Guid requestId)
        {
            var request= await _unit.Request.GetById(requestId);
            if (request != null)
            {
                return request;
            }
            return null;
        }
      
        public async Task<bool> UpdateRequest(BookingRequest request)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id == request.Request_Id);
            if (m_update != null)
            {
                m_update.Book_Id = request.Book_Id;
                m_update.Request_Image_Url = request.Request_Image_Url;
                m_update.Request_Book_Name= request.Request_Book_Name;
                m_update.Request_Quantity = request.Request_Quantity;
                m_update.Request_Price = request.Request_Price;
                m_update.Request_Amount = request.Request_Amount;
                m_update.Request_Date = DateTime.Now;
                m_update.Request_Date_Done = DateTime.Now;
                m_update.Request_Note = request.Request_Note;
                m_update.Is_Request_Status = 1;
                m_update.Is_RequestBook_Status=request.Is_RequestBook_Status;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<bool> DeleteRequest(Guid requestId)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id==requestId);
            if (m_update != null)
            {
                m_update.Is_Request_Status = 0;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<bool> RestoreRequest(Guid requestId)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id == requestId);
            if (m_update != null)
            {
                m_update.Is_Request_Status = 2;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RemoveRequest(Guid requestId)
        {
            var user = await _unit.Request.GetById(requestId);
            if (user != null)
            {
                _unit.Request.Delete(user);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> UpdateRequestUnDone(Guid requestId, string note)
        {
            var m_update = await _unit.Request.GetById(requestId);
            if (m_update != null)
            {
                m_update.Request_Note = note;
                m_update.Is_Request_Status = 3;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}

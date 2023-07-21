using BookStoreAPI.Core.DTO;
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
    public class BookService : IBookService
    {
        IUnitOfWorkRepository _unit;
        ICategoryRepository _cate;
        IImageService _image;
        Book m_book;
        
        public BookService(IUnitOfWorkRepository unit, ICategoryRepository cate,IImageService image)
        {
            _unit = unit;
            _cate = cate;
            _image = image;
            m_book = new Book();
        }
        public async Task<bool> CreateBook(Book book, string url, Guid Request_Id)
        {
            if (book != null)
            {
               // var m_list = await GetAllBook();
                book.Book_Id = Guid.NewGuid();
                book.Is_Book_Status= true;
                book.Book_Description = "No Data, Please Write Description";
                await _unit.Books.Add(book);
                var result = _unit.Save();
                if (result > 0)
                {
                    //add image
                    await AddImage(url, book.Book_Id);
                    //add bookid vào request id
                   await AddBookIdToRequest(Request_Id,book.Book_Id);
                    return true;
                }
            }
            return false;
        }

        private async Task AddImage(string url, Guid book_Id)
        {
            var image = new ImageBook();
            image.Image_URL = url;
            image.Book_Id = book_Id;
            await _image.CreateImage(image);
        }

        private async Task AddBookIdToRequest(Guid request_Id, Guid book_Id)
        {
           var request=await _unit.Request.GetById(request_Id);
            if (request != null)
            {
                request.Book_Id = book_Id;
                request.Is_RequestBook_Status = false;
                 _unit.Request.Update(request);
                _unit.Save();
            }
        }

        public async Task<bool> DeleteBook(Guid bookId)
        {
            var m_update = _unit.Books.SingleOrDefault(m_book, u => u.Book_Id==bookId);
            if (m_update != null)
            {
                m_update.Is_Book_Status = false;
                _unit.Books.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBook()
        {
            var bookList = await GetBook();
            var listDTO = new List<BookDTO>();
            listDTO =await GetDisplay(bookList, listDTO);
            return listDTO;
        }
       
       
        private async Task<List<BookDTO>> GetDisplay(IEnumerable<Book> bookList, List<BookDTO> listDTO)
        {
            var imageList = await _unit.Images.GetAll();
            var categoryList= await _unit.Category.GetAll();
            foreach(var item in bookList)
            {
                var dto= new BookDTO();
                dto.Book_Id=item.Book_Id;
                dto.Category_Id=item.Category_Id;
                dto.Category_Name = GetCategoryName(categoryList, item.Category_Id);
                dto.Image_URL = GetUrl(imageList, item.Book_Id);
                dto.Book_Author = item.Book_Author;
                dto.Book_Price = item.Book_Price;
                dto.Book_Quantity = item.Book_Quantity;
                dto.Book_Year_Public = item.Book_Year_Public;
                dto.Book_ISBN = item.Book_ISBN;
                dto.Book_Title= item.Book_Title;
                dto.Book_Description = item.Book_Description;
                dto.Is_Book_Status = item.Is_Book_Status;
                listDTO.Add(dto);
            }
            return listDTO;
        }

        private string GetUrl(IEnumerable<ImageBook> listImage, Guid book_Id)
        {
            var url = (from i in listImage where i.Book_Id == book_Id select i.Image_URL).FirstOrDefault();
            return url;
        }
        private List<string> GetUrlList(IEnumerable<ImageBook> listImage, Guid book_Id)
        {
            var url = from i in listImage where i.Book_Id == book_Id select i.Image_URL;
            return url.ToList();
        }

        private string GetCategoryName(IEnumerable<Category> categoryList, int category_Id)
        {
            var name = (from i in categoryList where i.Category_Id == category_Id select i.Category_Name).FirstOrDefault();
            return name;
        }

        private async Task<IEnumerable<Book>> GetBook()
        {
            var result = await _unit.Books.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<BookDetailDTO> GetBookById(Guid bookId)
        {
            var result=await _unit.Books.GetById(bookId);
            var listCate = await _cate.GetAll();
            var imageList = await _unit.Images.GetAll();
            if (result != null)
            {
                var bookDetail = new BookDetailDTO();
                //lấy thuộc tính gắn vào book detail
                bookDetail.Book_Id = result.Book_Id;
                bookDetail.Book_Title = result.Book_Title;
                bookDetail.Book_Description = result.Book_Description;
                bookDetail.Category_Id=result.Category_Id;
                bookDetail.Category_Name = GetCategoryName(listCate, result.Category_Id);
                bookDetail.Image_URL = GetUrlList(imageList, result.Book_Id);
                bookDetail.Book_Author= result.Book_Author;
                bookDetail.Book_Price= result.Book_Price;
                bookDetail.Book_Quantity= result.Book_Quantity;
                bookDetail.Book_Year_Public= result.Book_Year_Public;
                bookDetail.Book_ISBN= result.Book_ISBN;
                bookDetail.Is_Book_Status= result.Is_Book_Status;
                return bookDetail;
            }
            return null;
        }
       

        public async Task<IEnumerable<Book>> GetBookByName(string bookName)
        {
            //var result = await _unit.Books.GetByName(bookName);
            var books = await GetBook();
            var result = from b in books where (b.Book_Title.ToLower().Trim().Contains
                         (bookName.ToLower().Trim()) && b.Is_Book_Status==true) select b;
            if (result.Count()>0)
            {
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var m_update = _unit.Books.SingleOrDefault(m_book, u => u.Book_Id == book.Book_Id);
            if (m_update != null)
            {
                m_update.Category_Id= book.Category_Id;
                m_update.Book_Title= book.Book_Title;
                m_update.Book_Author= book.Book_Author;
                m_update.Book_Price= book.Book_Price;
                m_update.Book_Quantity= book.Book_Quantity;
                m_update.Book_Description= book.Book_Description;
                m_update.Book_Price=book.Book_Price;
                m_update.Book_Year_Public= 2023;
                m_update.Is_Book_Status= book.Is_Book_Status;
                _unit.Books.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreBook(Guid bookId)
        {
            var m_update = _unit.Books.SingleOrDefault(m_book, u => u.Book_Id == bookId);
            if (m_update != null)
            {
                m_update.Is_Book_Status = true;
                _unit.Books.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<BookDTO>> GetBookByCategory(int cateId)
        {
            var bookList = await GetBook();
            var bookListByCateId= from b in bookList where b.Category_Id == cateId select b;
            var listDTO = new List<BookDTO>();
            listDTO = await GetDisplay(bookListByCateId, listDTO);
            return listDTO;
        }

        public async  Task<bool> RemoveBook(Guid bookId)
        {
            var m_update = _unit.Books.SingleOrDefault(m_book, u => u.Book_Id == bookId);
            if (m_update != null)
            {
                _unit.Books.Delete(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}

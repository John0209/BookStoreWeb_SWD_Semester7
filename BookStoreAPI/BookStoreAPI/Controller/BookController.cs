using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service.IService;

namespace BookStoreAPI.Controller
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookService _book;
        IMapper _mapper;
        public BookController(IBookService book, IMapper mapper) 
        {
            _book = book;
            _mapper = mapper;
        }
        
        [HttpGet("getBook")]
        public async Task<IActionResult> GetAllBook()
        {
                var respone= await _book.GetAllBook();
                if(respone != null)
                {
                    return Ok(respone);
                }
            return BadRequest("null");
        }
        [HttpGet("getBookByCategory")]
        public async Task<IActionResult> GetAllBookByCategory(int categoryId)
        {
            var respone = await _book.GetBookByCategory(categoryId);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("null");
        }
        [HttpGet("searchBook")]
        public async Task<IActionResult> SearchBook(string nameBook)
        {
            var respone = await _book.GetBookByName(nameBook);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("Book don't exists");
        }
        [HttpGet("getBookDetail")]
        public async Task<IActionResult> GetBookDetail(Guid bookId)
        {
            var respone = await _book.GetBookById(bookId);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("Book don't exists");
        }
        [HttpPost("createBook")]
        public async Task<IActionResult> CreateBook(BookDTO dTO)
        {
            if (dTO != null)
            {
                var book = _mapper.Map<Book>(dTO);
                var result = await _book.CreateBook(book, dTO.Image_URL, dTO.Request_Id);
                if (result) return Ok("Create Book Success");
            }
            return BadRequest("Create Book Fail");
        }
        [HttpPut("updateBook")]
        public async Task<IActionResult> UpdateBook(BookDetailDTO bookDTO)
        {
            if (bookDTO != null)
            {
                var book = _mapper.Map<Book>(bookDTO);
                var result = await _book.UpdateBook(book);
                if (result) return Ok("Update Book Success");
            }
            return BadRequest("Update Book Fail");
        }
        [HttpPatch("deleteBook")]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
                var result = await _book.DeleteBook(bookId);
                if (result) return Ok("Delete Book Success");
                 return BadRequest("Delete Book Fail");
        }
        [HttpPatch("restoreBook")]
        public async Task<IActionResult> RestoreBook(Guid bookId)
        {
            var result = await _book.RestoreBook(bookId);
            if (result) return Ok("Restore Book Success");
            return BadRequest("Restore Book Fail");
        }
       
        [HttpDelete("removeBook")]
        public async Task<IActionResult> RemoveBook(Guid bookId)
        {
            var result = await _book.RemoveBook(bookId);
            if (result) return Ok("remove Book Success");
            return BadRequest("remove Book Fail");
        }
    }
}

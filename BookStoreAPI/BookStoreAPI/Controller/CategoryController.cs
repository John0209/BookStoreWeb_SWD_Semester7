using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service.IService;

namespace BookStoreAPI.Controller
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _cate;
        public CategoryController(ICategoryService cate) 
        {
            _cate = cate;
        }    
        [HttpGet("getCategory")]
        public async Task<IActionResult> GetCategory()
        {
                var respone= await _cate.GetAllCategory();
                if(respone != null)
                {
                    return Ok(respone);
                }
            return BadRequest("null");
        }
        [HttpGet("getCategoryById")]
        public async Task<IActionResult> GetCategoryById(int CategoryId)
        {
            var respone = await _cate.GetCategoryById(CategoryId);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("Category don't exists!");
        }
        

    }
}

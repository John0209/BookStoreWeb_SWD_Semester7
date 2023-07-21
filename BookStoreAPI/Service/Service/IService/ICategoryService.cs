using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int categoryId);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(string categoryId);
    }
}

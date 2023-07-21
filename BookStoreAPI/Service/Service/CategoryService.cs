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
    public class CategoryService : ICategoryService
    {
        IUnitOfWorkRepository _unit;
        public CategoryService(IUnitOfWorkRepository unit)
        {
            _unit = unit;
        }

        public Task<bool> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var result = await _unit.Category.GetAll();
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var result= await _unit.Category.GetById(categoryId);
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public Task<bool> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}

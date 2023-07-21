using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task AddList(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity);
        T SingleOrDefault(T entity, Func<T, bool> function);
        Task<IEnumerable<T>> TakePage(int number, IEnumerable<T> list);

    }
}

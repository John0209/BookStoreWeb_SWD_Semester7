using BookStoreAPI.Core.Interface;
using BookStoreAPI.Infracstructure.Helper;
using Microsoft.EntityFrameworkCore;
using PawnShopBE.Core.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Infracstructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;
        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddList(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public T SingleOrDefault(T entity, Func<T, bool> function)
        {
            return _dbContext.Set<T>().SingleOrDefault(function);
        }

        public async Task<IEnumerable<T>> TakePage(int number, IEnumerable<T> list)
        {
            var numPage = (int)NumberPage.numPage;
            var skip = (numPage * number) - numPage;
            return list.Skip(skip).Take(numPage);
        }
        
    }
}

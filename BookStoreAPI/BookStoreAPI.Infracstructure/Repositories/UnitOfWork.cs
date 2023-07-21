using BookStoreAPI.Core.Interface;
using BookStoreAPI.Infracstructure.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Infracstructure.Repositories
{
    public class UnitOfWork : IUnitOfWorkRepository
    {
        private readonly DbContextClass _dbContextClass;
        public IBookingRequestRepository Request { get; }
        public IBookRepository Books { get; }

        public ICategoryRepository Category { get; }

        public IImageBookRepository Images { get; }

        public IImportationRepository Importation { get; }

        public IImportationDetailRepository ImportationDetail { get; }

        public IInventoryRepository Inventory { get; }

        public IOrderDetailRepository OrderDetail { get; }

        public IOrderRepository Order { get; }

        public IRoleRepository Role { get; }
        public IUserRepository User { get; }

        public UnitOfWork(DbContextClass dbContextClass, IBookingRequestRepository request, IBookRepository books, ICategoryRepository category, 
            IImageBookRepository images, IImportationRepository importation, IImportationDetailRepository importationDetail, 
             IInventoryRepository inventory, IOrderDetailRepository orderDetail, IOrderRepository order,
            IRoleRepository role, IUserRepository user)
        {
            _dbContextClass = dbContextClass;
            Request = request;
            Books = books;
            Category = category;
            Images = images;
            Importation = importation;
            ImportationDetail = importationDetail;
            Inventory = inventory;
            OrderDetail = orderDetail;
            Order = order;
            Role = role;
            User = user;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContextClass.Dispose();
            }
        }

        public int Save()
        {
            return _dbContextClass.SaveChanges();
        }

        public Task<int> SaveList()
        {
            return _dbContextClass.SaveChangesAsync();
        }
    }
}

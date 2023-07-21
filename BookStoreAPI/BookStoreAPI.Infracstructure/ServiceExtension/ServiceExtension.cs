using BookStoreAPI.Core.Interface;
using BookStoreAPI.Infracstructure.Helper;
using BookStoreAPI.Infracstructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Infracstructure.ServiceExtension;

public static class ServiceExtension
{
    public static IServiceCollection AddDIServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DbContextClass>(option =>
        {
            object value = option.UseSqlServer(configuration.GetConnectionString("BookStore"));
        });
        services.AddScoped<IUnitOfWorkRepository, UnitOfWork>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookingRequestRepository, RequestRepository>();
        services.AddScoped<IImageBookRepository, ImageRepository>();
        services.AddScoped<IImportationDetailRepository, ImportationDetailRepository>();
        services.AddScoped<IImportationRepository, ImportationRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRoleRepository,RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

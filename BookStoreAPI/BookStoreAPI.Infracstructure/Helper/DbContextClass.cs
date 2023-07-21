using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreAPI.Core.Model;

namespace BookStoreAPI.Infracstructure.Helper;

public class DbContextClass : DbContext
{
    public DbContextClass()
    {

    }
    public DbContextClass(DbContextOptions<DbContextClass> options):base(options)
    {

    }
    #region DbSet
    public DbSet<Book> Books { get; set; }
    public DbSet<BookingRequest> Requests { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<ImageBook> ImageBooksBooks { get; set; }
    public DbSet<Importation> Importations { get; set; }
    public DbSet<ImportationDetail> ImportationDetails { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(r => r.Role_Id);
            entity.Property(r => r.Role_Name).HasColumnType("nvarchar(30)");
        });
        modelBuilder.Entity<ImageBook>(entity =>
        {
            entity.ToTable("Image");
            entity.HasKey(r => r.Image_Id);
            entity.HasOne(r => r.Book).WithMany(y => y.Image_Book).HasForeignKey(y => y.Book_Id).IsRequired(true);
        });
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
            entity.HasKey(r => r.Category_Id);
            entity.Property(r => r.Category_Name).HasColumnType("nvarchar(50)");
        });
        
        
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");
            entity.HasKey(y => y.Inventory_Id);
            entity.HasOne(y => y.User).WithMany(y => y.Inventory).HasForeignKey(y => y.User_Id).IsRequired(true);
            entity.HasOne(y => y.Books).WithMany(y => y.Inventory).HasForeignKey(y => y.Book_Id).IsRequired(true);
            entity.Property(r => r.Inventory_Note).HasColumnType("nvarchar(100)");

        });
        modelBuilder.Entity<BookingRequest>(entity =>
        {
            entity.ToTable("Request");
            entity.HasKey(y => y.Request_Id);
            entity.HasOne(y => y.Book).WithMany(y => y.BookingRequest).HasForeignKey(y => y.Book_Id).IsRequired(false);
            entity.Property(y=>y.Request_Note).IsRequired(false).HasColumnType("nvarchar(100)");
            entity.Property(r => r.Request_Book_Name).HasColumnType("nvarchar(120)");

        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(y => y.User_Id);
            entity.HasOne(y => y.Role).WithMany(y => y.Users).HasForeignKey(y=>y.Role_Id).IsRequired(true);
            entity.Property(y => y.User_Account).IsRequired(true).HasColumnType("varchar(30)");
            entity.Property(y=> y.User_Password).IsRequired(true).HasColumnType("varchar(100)");
            entity.Property(y => y.User_Phone).IsRequired(false).HasColumnType("varchar(15)");
            entity.Property(y => y.Is_User_Gender).IsRequired(false).HasColumnType("varchar(10)");
            entity.Property(y => y.User_Address).IsRequired(false).HasColumnType("nvarchar(100)");
            entity.Property(y => y.User_Name).IsRequired(false).HasColumnType("nvarchar(30)");
            entity.Property(y => y.User_Email).IsRequired(false).HasColumnType("varchar(40)");
        });
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");
            entity.HasKey(y => y.Book_Id);
            entity.HasOne(y => y.Category).WithMany(y => y.Books).HasForeignKey(y => y.Category_Id).IsRequired(true);
            entity.Property(r => r.Book_Title).HasColumnType("nvarchar(120)");
            entity.Property(r => r.Book_Author).HasColumnType("nvarchar(30)");

        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(y => y.Order_Id);
            entity.HasOne(y => y.User).WithMany(y => y.Order).HasForeignKey(y => y.User_Id).IsRequired(true);
            entity.Property(r => r.Order_Customer_Name).HasColumnType("nvarchar(30)");
            entity.Property(r => r.Order_Customer_Address).HasColumnType("nvarchar(100)");
            entity.Property(r => r.Order_Customer_Phone).HasColumnType("varchar(20)");
            entity.Property(r => r.Order_Code).HasColumnType("varchar(15)");
        });
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");
            entity.HasKey(y => y.Order_Detail_Id);
            entity.HasOne(y => y.Order).WithMany(y => y.OrderDetails).HasForeignKey(y => y.Order_Id).IsRequired(true);
            entity.HasOne(y => y.Book).WithMany(y => y.Order_Detail).HasForeignKey(y => y.Book_Id).IsRequired(true);
        });
        modelBuilder.Entity<Importation>(entity =>
        {
            entity.ToTable("Importation");
            entity.HasKey(y => y.Import_Id);
            entity.HasOne(y => y.User).WithMany(y => y.Importation).HasForeignKey(y => y.User_Id).IsRequired(true);


        });
        modelBuilder.Entity<ImportationDetail>(entity =>
        {
            entity.ToTable("ImportationDetail");
            entity.HasKey(y => y.Import_Detail_Id);
            entity.HasOne(y => y.Book).WithMany(y => y.Importation_Detail).HasForeignKey(y => y.Book_Id).IsRequired(true);
            entity.HasOne(y => y.Importation).WithMany(y => y.ImportationDetails).HasForeignKey(y => y.Import_Id).IsRequired(true);
        });

    }

}

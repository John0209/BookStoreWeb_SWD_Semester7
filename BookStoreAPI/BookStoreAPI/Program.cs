using BookStoreAPI.Core.Model;
using BookStoreAPI.Infracstructure.ServiceExtension;
using Service.Service;
using Service.Service.IService;
using AutoMapper;
using BookStoreAPI.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDIServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Scoped
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImportationService, ImportationService>();
builder.Services.AddScoped<IImportationDetailService, ImportationDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "bookstore");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

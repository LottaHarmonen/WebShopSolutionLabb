using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories.Order;
using WebShop.DataAccess.Repositories.Product;
using WebShop.DataAccess.Repositories.User;
using WebShop.Notifications;
using WebShop.Services.Order;
using WebShop.Services.Product;
using WebShop.Services.User;
using WebShop.UnitOfWork;
using WebShopSolution.Application.Factories;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Register the Controllers (API layer)
builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<OrderFactoryManager>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<INotificationObserver, EmailNotification>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add authentication and authorization middleware if needed
app.UseAuthorization();

// Map controllers to handle requests
app.MapControllers();

// Run the application
app.Run();
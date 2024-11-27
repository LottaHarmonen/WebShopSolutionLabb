using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Order;
using WebShop.DataAccess.Repositories.Product;
using WebShop.DataAccess.Repositories.User;
using WebShop.Notifications;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; }

        private readonly MyDbContext _dbContext;

        private readonly ProductSubject _productSubject;

        public UnitOfWork(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            MyDbContext dbContext,
            ProductSubject productSubject = null

        )
        {
            _dbContext = dbContext;
            Customers = customerRepository;
            Products = productRepository;
            Orders = orderRepository;

            _productSubject = productSubject ?? new ProductSubject();
            _productSubject.Attach(new EmailNotification());
        }


        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_dbContext); 
        }

        public async Task NotifyProductAdded(Product product)
        {
           _productSubject.Notify(product);
        }

        public async Task Complete()
        {
            try
            {
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException("An error occurred while committing changes to the database.", ex);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

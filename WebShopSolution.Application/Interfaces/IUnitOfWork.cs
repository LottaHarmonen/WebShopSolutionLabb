
using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Order;
using WebShop.DataAccess.Repositories.Product;
using WebShop.DataAccess.Repositories.User;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IRepository<T> Repository<T>() where T : class;

        Task NotifyProductAdded(Product product); 
        Task Complete();
    }
}


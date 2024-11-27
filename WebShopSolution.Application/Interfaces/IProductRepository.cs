namespace WebShop.DataAccess.Repositories.Product;

public interface IProductRepository : IRepository<WebShop.Product>
{
    Task<WebShop.Product?> GetProductByCategory(string name);

}
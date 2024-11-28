namespace WebShop.Services.Product;

public interface IProductService : IGenericService<WebShop.Product>
{
    Task<WebShop.Product> GetProductByCategoryAsync(string name);
    Task AddProduct(WebShop.Product product);
}
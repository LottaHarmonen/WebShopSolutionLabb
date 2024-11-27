using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Product;
using WebShop.UnitOfWork;

namespace WebShop.Services.Product;

public class ProductService(IUnitOfWork unitOfWork) : GenericService<WebShop.Product>(unitOfWork), IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<WebShop.Product> GetProductByCategoryAsync(string name)
    {
        return await _unitOfWork.Products.GetProductByCategory(name);
    }
}
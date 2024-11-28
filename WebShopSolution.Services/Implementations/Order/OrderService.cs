using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Order;
using WebShop.UnitOfWork;

namespace WebShop.Services.Order;

public class OrderService(IUnitOfWork unitOfWork) : GenericService<WebShop.Order>(unitOfWork), IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddOrderWithProductValidation(WebShop.Order order)
    {
        var validatedProducts = new List<WebShop.Product>();

        foreach (var product in order.Products)
        {
            var fetchedProduct = await _unitOfWork.Products.GetById(product.Id);
            if (fetchedProduct == null)
            {
                throw new ArgumentException($"Product with ID {product.Id} does not exist.");
            }

            validatedProducts.Add(fetchedProduct);
        }

        order.Products.Clear();
        foreach (var validatedProduct in validatedProducts)
        {
            order.Products.Add(validatedProduct);
        }

        await Add(order);
    }

    public override async Task<WebShop.Order> Get(int id)
    {
        return await _unitOfWork.Orders.GetById(id);
    }

    public override async Task<IEnumerable<WebShop.Order?>>? GetAll()
    {
        return await _unitOfWork.Orders.GetAll();
    }

}


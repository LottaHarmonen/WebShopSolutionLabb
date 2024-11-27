using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Order;
using WebShop.UnitOfWork;

namespace WebShop.Services.Order;

public class OrderService(IUnitOfWork unitOfWork) : GenericService<WebShop.Order>(unitOfWork), IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task AddOrderWithProductValidation(WebShop.Order order)
    {
        foreach (var product in order.Products)
        {
            var fetchedProduct = await _unitOfWork.Products.GetById(product.Id);
            if (fetchedProduct == null)
            {
                throw new ArgumentException("Product does not exist.");
            }
        }

        await _unitOfWork.Orders.Add(order);
        await _unitOfWork.Complete();
    }
}


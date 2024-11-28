using WebShop.DataAccess.Repositories;
using WebShop.DataAccess.Repositories.Order;
using WebShop.UnitOfWork;
using WebShopSolution.Application.Factories;

namespace WebShop.Services.Order;

public class OrderService(IUnitOfWork unitOfWork, OrderFactoryManager? orderFactoryManager = null) : GenericService<WebShop.Order>(unitOfWork), IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly OrderFactoryManager _orderFactoryManager = orderFactoryManager;

    public override async Task Add(WebShop.Order order)
    {
        //Abstract factory
        var factory = _orderFactoryManager.GetFactory(order.OrderType);
        var newOrder = factory.CreateOrder(order);

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

        newOrder.Products = validatedProducts;

        await _unitOfWork.Orders.Add(newOrder);
        await _unitOfWork.Complete();
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


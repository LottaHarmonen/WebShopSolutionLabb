namespace WebShop.Services.Order;

public interface IOrderService : IGenericService<WebShop.Order>
{
    Task AddOrderWithProductValidation(WebShop.Order order);
}
namespace WebShop.Services.Order;

public interface IOrderService : IGenericService<WebShop.Order>
{
    Task Add(WebShop.Order order);
}
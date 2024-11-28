using WebShop;

namespace WebShopSolution.Application.Factories;

public interface IOrderFactory
{
    WebShop.Order CreateOrder(Order order);
}
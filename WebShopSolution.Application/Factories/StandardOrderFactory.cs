using WebShop;

namespace WebShopSolution.Application.Factories;

public class StandardOrderFactory : IOrderFactory
{
    public WebShop.Order CreateOrder(Order order)
    {
        order.OrderType = order.OrderType;
        order.Discount = 0;

        return order;
    }
}
using WebShop;

namespace WebShopSolution.Application.Factories;

public class ExpressOrderFactory : IOrderFactory
{
    public WebShop.Order CreateOrder(Order order)
    {
        order.OrderType = "Express";
        order.Discount = 10;

        return order;
    }
}
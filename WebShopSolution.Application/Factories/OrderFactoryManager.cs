namespace WebShopSolution.Application.Factories;

public class OrderFactoryManager
{
    public IOrderFactory GetFactory(string orderType)
    {
        switch (orderType.ToLower())
        {
            case "express":
                return new ExpressOrderFactory();
            case "standard":
            default:
                return new StandardOrderFactory();
        }
    }
}
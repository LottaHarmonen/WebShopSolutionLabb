using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories.Order;

public class OrderRepository(MyDbContext dbContext) : Repository<WebShop.Order>(dbContext), IOrderRepository
{

}
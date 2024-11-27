using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories.User;

public class CustomerRepository(MyDbContext dbContext) : Repository<WebShop.Customer>(dbContext), ICustomerRepository
{
    private readonly MyDbContext _dbContext = dbContext;
}
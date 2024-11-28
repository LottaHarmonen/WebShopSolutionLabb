using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories.Order;

public class OrderRepository : Repository<WebShop.Order>, IOrderRepository
{
    public OrderRepository(MyDbContext dbContext) : base(dbContext)
    {
    }
    public override async Task<IEnumerable<WebShop.Order>> GetAll()
    {
        return await _dbSet
            .Include(o => o.Products) 
            .ToListAsync();
    }

    public override async Task<WebShop.Order?> GetById(int id)
    {
        return await _dbSet
            .Include(o => o.Products) 
            .FirstOrDefaultAsync(o => o.Id == id);
    }



}
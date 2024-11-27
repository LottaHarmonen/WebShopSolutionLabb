using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories.Product;

public class ProductRepository : Repository<WebShop.Product>, IProductRepository
{
    private readonly MyDbContext _context;

    public ProductRepository(MyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<WebShop.Product?> GetProductByCategory(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p=> p.Name == name);
    }
}
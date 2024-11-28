using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet;
    private readonly MyDbContext _dbContext;

    public Repository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T?>>? GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Update(T? entity)
    {
        if (entity != null) _dbSet.Update(entity);
    }

    public async Task Delete(int id)
    {
        var objectToDelete = await _dbSet.FindAsync(id);
        if (objectToDelete != null)
        {
            _dbSet.Remove(objectToDelete);
        }

    }
}
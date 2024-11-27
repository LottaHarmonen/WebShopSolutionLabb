using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Data;

namespace WebShop.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    public Repository(MyDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T?>>? GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Update(T? entity)
    {
        //_dbSet.Attach(entity);
        //_dbContext.Entry(entity).State = EntityState.Modified;

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
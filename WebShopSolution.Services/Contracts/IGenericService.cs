namespace WebShop.Services;

public interface IGenericService<T> where T : class
{
    Task Add(T? entity);
    Task<IEnumerable<T?>>? GetAll();
    Task<T?> Get(int id);
    Task Update(T? Entity);
    Task Delete(int id);
}
using WebShop.DataAccess.Repositories;
using WebShop.UnitOfWork;

namespace WebShop.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public GenericService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = _unitOfWork.Repository<T>();
    }

    public virtual async Task Add(T? entity)
    {
        await _repository.Add(entity);
        await _unitOfWork.Complete();
    }

    public virtual Task<IEnumerable<T?>>? GetAll()
    {
        return _repository.GetAll();
    }

    public virtual Task<T?> Get(int id)
    {
        return _repository.GetById(id);
    }

    public async Task Update(T? entity)
    {
        await _repository.Update(entity);
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
        await _unitOfWork.Complete();
    }
}
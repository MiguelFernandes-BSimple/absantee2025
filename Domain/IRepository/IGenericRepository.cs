namespace Domain.IRepository;

using System.Linq.Expressions;

public interface IGenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
{
    TInterface? GetById(Guid id);
    Task<TInterface?> GetByIdAsync(Guid id);
    IEnumerable<TInterface> GetAll();
    Task<IEnumerable<TInterface>> GetAllAsync();
    TInterface Add(TInterface entity);
    Task<TInterface> AddAsync(TInterface entity);
    void AddRange(IEnumerable<TInterface> entities);
    Task AddRangeAsync(IEnumerable<TInterface> entities);
    void Remove(TInterface entity);
    Task RemoveAsync(TInterface entity);
    void RemoveRange(IEnumerable<TInterface> entities);
    Task RemoveRangeAsync(IEnumerable<TInterface> entities);
    Task<int> SaveChangesAsync();
}
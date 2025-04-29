namespace Domain.IRepository;

using System.Linq.Expressions;

public interface IGenericRepository<TDomain, TDataModel> where TDomain : class where TDataModel : class
{
    TDomain? GetById(Guid id);
    Task<TDomain?> GetByIdAsync(Guid id);
    IEnumerable<TDomain> GetAll();
    Task<IEnumerable<TDomain>> GetAllAsync();
    TDomain Add(TDomain entity);
    Task AddAsync(TDomain entity);
    void AddRange(IEnumerable<TDomain> entities);
    Task AddRangeAsync(IEnumerable<TDomain> entities);
    void Remove(TDomain entity);
    Task RemoveAsync(TDomain entity);
    void RemoveRange(IEnumerable<TDomain> entities);
    Task RemoveRangeAsync(IEnumerable<TDomain> entities);
    Task<int> SaveChangesAsync();
}
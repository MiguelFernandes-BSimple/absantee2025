namespace Domain.IRepository;

using System.Linq.Expressions;

public interface IGenericRepository<TDomain, TDataModel> where TDomain : class where TDataModel : class
{
    TDomain? GetById(long id);
    IEnumerable<TDomain> GetAll();
    void Add(TDomain entity);
    void AddRange(IEnumerable<TDomain> entities);
    void Remove(TDomain entity);
    void RemoveRange(IEnumerable<TDomain> entities);
    Task<int> SaveChangesAsync();
}
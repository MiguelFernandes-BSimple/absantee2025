using System.Linq.Expressions;
using Domain.Interfaces;
using Domain.IRepository;

public class UserRepository : IUserRepository
{
    public void Add(IUser entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<IUser> entities)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IUser> Find(Expression<Func<IUser, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IUser> GetAll()
    {
        throw new NotImplementedException();
    }

    public IUser? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IUser? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Remove(IUser entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<IUser> entities)
    {
        throw new NotImplementedException();
    }
}
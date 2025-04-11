using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IProjectRepository : IGenericRepository<IProject, IProjectVisitor>
{
}

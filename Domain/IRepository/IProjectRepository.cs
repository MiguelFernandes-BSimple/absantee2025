using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IProjectRepository : IGenericRepository<Project, IProjectVisitor>
{
    public Task<IEnumerable<Project>> GetByIdAsync(IEnumerable<Guid> projectIds);
    public Task<bool> CheckIfAcronymIsUnique(string acronym);
}

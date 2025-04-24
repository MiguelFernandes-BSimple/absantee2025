using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<ICollaborator, ICollaboratorVisitor>
{
    Task<bool> IsRepeated(ICollaborator collaborator);
    Task<IEnumerable<ICollaborator>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<ICollaborator>> GetByUsersIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<ICollaborator>> GetActiveCollaborators();
}
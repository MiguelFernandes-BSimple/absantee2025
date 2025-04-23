using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<Collaborator, ICollaboratorVisitor>
{
    Task<bool> IsRepeated(ICollaborator collaborator);
    Task<IEnumerable<ICollaborator>> GetByIdsAsync(IEnumerable<long> ids);
    Task<IEnumerable<ICollaborator>> GetByUsersIdsAsync(IEnumerable<long> ids);
    Task<IEnumerable<ICollaborator>> GetActiveCollaborators();
}
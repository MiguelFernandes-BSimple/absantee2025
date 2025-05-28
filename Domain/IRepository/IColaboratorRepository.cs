using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ICollaboratorRepository : IGenericRepository<Collaborator, ICollaboratorVisitor>
{
    Task<bool> IsRepeated(ICollaborator collaborator);
    Task<IEnumerable<Collaborator>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Collaborator>> GetByUsersIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Collaborator>> GetActiveCollaborators();
    Task<long> GetCount();
    public Collaborator? UpdateCollaborator(Collaborator collab);
    Task<Collaborator?> GetByIdAsNoTrackingAsync(Guid id);
}
using Domain.Visitor;
using Domain.Interfaces;

namespace Domain.IRepository;

public interface IAssociationFormationModuleCollaboratorRepository : IGenericRepository<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor>
{
    public Task<IEnumerable<IAssociationFormationModuleCollaborator>> FindAllByFormationModuleAsync(long formationModuleId);

}
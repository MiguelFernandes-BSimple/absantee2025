using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;
public interface IAssociationTrainingModuleCollaboratorRepository : IGenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>
{
    public Task<IAssociationTrainingModuleCollaborator?> FindByCollaborator(long collabId);
    public Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllCollaboratorsByTrainingModule(long tmId);
}
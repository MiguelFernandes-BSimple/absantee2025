using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface IAssociationTrainingModuleCollaboratorRepository : IGenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>
{
    public Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllCollaboratorsByTrainingModule(long tmId);
}
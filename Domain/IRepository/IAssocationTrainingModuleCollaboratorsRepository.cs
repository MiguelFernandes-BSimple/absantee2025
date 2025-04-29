using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface IAssociationTrainingModuleCollaboratorsRepository : IGenericRepository<AssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>
{
    Task<IEnumerable<AssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<Guid> trainingModuleIds);
}

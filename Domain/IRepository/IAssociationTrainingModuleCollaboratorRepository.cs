using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationTrainingModuleCollaboratorRepository : IGenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> {
    Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByModuleIdAsync(long moduleId);
}

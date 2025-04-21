using Domain.Interfaces;
using Domain.Models;

namespace Domain.Factory;

public interface IAssociationTrainingModuleCollaboratorFactory {
    Task<AssociationTrainingModuleCollaborator> Create(long collabId, long moduleId);
    //AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor visitor);
}

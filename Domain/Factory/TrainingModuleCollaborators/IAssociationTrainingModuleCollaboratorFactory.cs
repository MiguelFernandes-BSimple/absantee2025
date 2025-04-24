using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IAssociationTrainingModuleCollaboratorFactory
    {
        Task<AssociationTrainingModuleCollaborator> Create(Guid trainingModuleId, Guid collaboratorId);
        AssociationTrainingModuleCollaborator Create(AssociationTrainingModuleCollaboratorVisitor visitor);
    }
}

using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IAssociationTrainingModuleCollaboratorFactory
    {
        Task<IAssociationTrainingModuleCollaborator> Create(Guid trainingModuleId, Guid collaboratorId);
        AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor visitor);
    }
}

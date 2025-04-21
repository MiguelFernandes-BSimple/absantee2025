using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public interface IAssociationCollaboratorTrainingModuleFactory
{
    public Task<AssociationCollaboratorTrainingModule> Create(long collaboratorId, long trainingModuleId, PeriodDate periodDate);

    public AssociationCollaboratorTrainingModule Create(IAssociationCollaboratorTrainingModuleVisitor associationCollaboratorTrainingModuleVisitor);
}
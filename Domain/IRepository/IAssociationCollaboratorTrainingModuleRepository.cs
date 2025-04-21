using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationCollaboratorTrainingModuleRepository : IGenericRepository<IAssociationCollaboratorTrainingModule,IAssociationCollaboratorTrainingModuleVisitor>
{
    public Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllByAsync(long collaboratorId);
    public Task<IAssociationCollaboratorTrainingModule?> FindByCollaboratorAndTrainingModuleAsync(long collaboratorId, long trainingModuleId);
    public Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllCollaboratorAndTrainingModuleAsync(long collaboratorId, long trainingModuleId);
    public Task<IEnumerable<IAssociationCollaboratorTrainingModule>> FindAllByTrainingModuleAndBetweenPeriodAsync(long trainingModuleId, PeriodDate periodDate);
    public Task<bool> CanInsert(long collaboratorId, long trainingModuleId, PeriodDate periodDate);
    
}
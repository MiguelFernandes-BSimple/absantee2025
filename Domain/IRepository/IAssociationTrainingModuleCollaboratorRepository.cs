using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IAssociationTrainingModuleCollaboratorRepository : IGenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>
{
    public Task<bool> CanInsert(long collaboratorId, long trainingModuleId, PeriodDateTime periodDateTime);
    public Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByTrainingModuleAsync(long trainingModuleId);
    public Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByTrainingModuleAfterDateAsync(long trainingModuleId, DateTime dateTime);
}

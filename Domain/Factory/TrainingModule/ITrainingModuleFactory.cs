using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface ITrainingModuleFactory
    {
        Task<ITrainingModule> Create(Guid traingSubjectId, List<PeriodDateTime> periods);
        TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
    }
}
